using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities.Identity;

namespace cdr_group.Application.Services
{
    public class UserService : BaseService<User, UserDto, CreateUserDto, UpdateUserDto>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<User> Repository => UnitOfWork.Users;

        public override async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await UnitOfWork.Users.GetUsersWithRolesAsync();
            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public override async Task<PagedResult<UserDto>> GetPagedAsync(PagedRequest request)
        {
            var (users, totalCount) = await UnitOfWork.Users.GetUsersWithRolesPagedAsync(request);
            var userDtos = Mapper.Map<List<UserDto>>(users);
            return new PagedResult<UserDto>(userDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await UnitOfWork.Users.GetWithRolesAsync(id);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            var user = await UnitOfWork.Users.GetByUsernameAsync(username);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await UnitOfWork.Users.GetByEmailAsync(email);
            return Mapper.Map<UserDto>(user);
        }

        protected override async Task ValidateCreateAsync(CreateUserDto dto)
        {
            if (await UnitOfWork.Users.UsernameExistsAsync(dto.Username))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            if (await UnitOfWork.Users.EmailExistsAsync(dto.Email))
            {
                throw new InvalidOperationException("Email already exists.");
            }
        }

        protected override Task BeforeCreateAsync(User entity, CreateUserDto dto)
        {
            entity.PasswordHash = HashPassword(dto.Password);
            return Task.CompletedTask;
        }

        protected override async Task AfterCreateAsync(User entity, CreateUserDto dto)
        {
            if (dto.RoleIds != null && dto.RoleIds.Any())
            {
                await AssignRolesToUserAsync(entity.Id, new AssignRolesDto { RoleIds = dto.RoleIds });
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateUserDto dto, User entity)
        {
            if (dto.Email != null && dto.Email != entity.Email)
            {
                if (await UnitOfWork.Users.EmailExistsAsync(dto.Email))
                {
                    throw new InvalidOperationException("Email already exists.");
                }
            }
        }

        public async Task<bool> ChangePasswordAsync(Guid id, ChangePasswordDto dto)
        {
            var user = await UnitOfWork.Users.GetByIdAsync(id);
            if (user == null) return false;

            if (!VerifyPassword(dto.CurrentPassword, user.PasswordHash))
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }

            user.PasswordHash = HashPassword(dto.NewPassword);
            await UnitOfWork.Users.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto?> AssignRolesToUserAsync(Guid userId, AssignRolesDto dto)
        {
            var user = await UnitOfWork.Users.GetWithRolesAsync(userId);
            if (user == null) return null;

            foreach (var roleId in dto.RoleIds)
            {
                if (!user.UserRoles.Any(ur => ur.RoleId == roleId && !ur.IsDeleted))
                {
                    var role = await UnitOfWork.Roles.GetByIdAsync(roleId);
                    if (role != null)
                    {
                        user.UserRoles.Add(new UserRole
                        {
                            Id = Guid.NewGuid(),
                            UserId = userId,
                            RoleId = roleId
                        });
                    }
                }
            }

            await UnitOfWork.SaveChangesAsync();
            return await GetByIdAsync(userId);
        }

        public async Task<UserDto?> RemoveRolesFromUserAsync(Guid userId, List<Guid> roleIds)
        {
            var user = await UnitOfWork.Users.GetWithRolesAsync(userId);
            if (user == null) return null;

            foreach (var userRole in user.UserRoles.Where(ur => roleIds.Contains(ur.RoleId)))
            {
                userRole.IsDeleted = true;
            }

            await UnitOfWork.SaveChangesAsync();
            return await GetByIdAsync(userId);
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await UnitOfWork.Users.GetByUsernameAsync(username);
            if (user == null || !user.IsActive) return false;

            return VerifyPassword(password, user.PasswordHash);
        }

        public async Task<bool> ActivateUserAsync(Guid id)
        {
            var user = await UnitOfWork.Users.GetByIdAsync(id);
            if (user == null) return false;

            user.IsActive = true;
            user.UpdatedAt = DateTime.UtcNow;
            await UnitOfWork.Users.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateUserAsync(Guid id)
        {
            var user = await UnitOfWork.Users.GetByIdAsync(id);
            if (user == null) return false;

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;
            await UnitOfWork.Users.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();
            return true;
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
