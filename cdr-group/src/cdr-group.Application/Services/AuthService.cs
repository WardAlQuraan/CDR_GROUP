using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using cdr_group.Application.Settings;
using cdr_group.Contracts.DTOs.Auth;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Persistence.Data;

namespace cdr_group.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthService(ApplicationDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<TokenDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Username == dto.Username && !u.IsDeleted);

            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
            {
                throw new InvalidOperationException("Invalid username or password.");
            }

            if (!user.IsActive)
            {
                throw new InvalidOperationException("User account is deactivated.");
            }

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
            {
                throw new InvalidOperationException("User account is locked.");
            }

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            user.FailedLoginAttempts = 0;
            await _context.SaveChangesAsync();

            return await GenerateTokensAsync(user);
        }

        public async Task<TokenDto> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username && !u.IsDeleted))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email && !u.IsDeleted))
            {
                throw new InvalidOperationException("Email already exists.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assign default "User" role
            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User" && !r.IsDeleted);
            if (userRole != null)
            {
                user.UserRoles.Add(new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RoleId = userRole.Id,
                    CreatedAt = DateTime.UtcNow
                });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Reload user with roles
            user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .FirstAsync(u => u.Id == user.Id);

            return await GenerateTokensAsync(user);
        }

        public async Task<TokenDto> RefreshTokenAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(rt => rt.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                            .ThenInclude(r => r.RolePermissions)
                                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsDeleted);

            if (token == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            if (!token.IsActive)
            {
                throw new UnauthorizedAccessException("Refresh token has expired or been revoked.");
            }

            if (!token.User.IsActive)
            {
                throw new UnauthorizedAccessException("User account is deactivated.");
            }

            // Revoke old token
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;

            // Generate new tokens
            var newTokens = await GenerateTokensAsync(token.User);

            // Update replaced by token reference
            token.ReplacedByToken = newTokens.RefreshToken;
            await _context.SaveChangesAsync();

            return newTokens;
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsDeleted);

            if (token == null)
            {
                throw new KeyNotFoundException("Refresh token not found.");
            }

            if (!token.IsActive)
            {
                return; // Already revoked or expired
            }

            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<UserInfoDto> GetCurrentUserAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            return MapToUserInfo(user);
        }

        public async Task ChangePasswordAsync(Guid userId, ChangePasswordRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (!VerifyPassword(dto.CurrentPassword, user.PasswordHash))
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }

            user.PasswordHash = HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        private async Task<TokenDto> GenerateTokensAsync(User user)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = await GenerateRefreshTokenAsync(user.Id);

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                User = MapToUserInfo(user)
            };
        }

        private string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add roles
            foreach (var userRole in user.UserRoles.Where(ur => !ur.IsDeleted))
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));

                // Add permissions from role
                foreach (var rolePermission in userRole.Role.RolePermissions.Where(rp => !rp.IsDeleted))
                {
                    claims.Add(new Claim("permission", rolePermission.Permission.Name));
                }
            }

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
        {
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = GenerateSecureToken(),
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                CreatedAt = DateTime.UtcNow
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }

        private static string GenerateSecureToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private static UserInfoDto MapToUserInfo(User user)
        {
            var roles = user.UserRoles
                .Where(ur => !ur.IsDeleted)
                .Select(ur => ur.Role.Name)
                .ToList();

            var permissions = user.UserRoles
                .Where(ur => !ur.IsDeleted)
                .SelectMany(ur => ur.Role.RolePermissions.Where(rp => !rp.IsDeleted))
                .Select(rp => rp.Permission.Name)
                .Distinct()
                .ToList();

            return new UserInfoDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles,
                Permissions = permissions
            };
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
