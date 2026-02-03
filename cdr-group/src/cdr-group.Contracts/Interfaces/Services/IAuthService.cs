using cdr_group.Contracts.DTOs.Auth;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto dto);
        Task<TokenDto> RegisterAsync(RegisterDto dto);
        Task<TokenDto> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string refreshToken);
        Task<UserInfoDto> GetCurrentUserAsync(Guid userId);
        Task ChangePasswordAsync(Guid userId, ChangePasswordRequestDto dto);
    }
}
