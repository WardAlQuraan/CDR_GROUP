using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Domain.Localization;

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
                throw new InvalidOperationException(Messages.UsernameExists);
            }

            if (await UnitOfWork.Users.EmailExistsAsync(dto.Email))
            {
                throw new InvalidOperationException(Messages.EmailExists);
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
                    throw new InvalidOperationException(Messages.EmailExists);
                }
            }
        }

        public async Task<bool> ChangePasswordAsync(Guid id, ChangePasswordDto dto)
        {
            var user = await UnitOfWork.Users.GetByIdAsync(id);
            if (user == null) return false;

            //if (!VerifyPassword(dto.CurrentPassword, user.PasswordHash))
            //{
            //    throw new InvalidOperationException(Messages.IncorrectCurrentPassword);
            //}

            user.PasswordHash = HashPassword(dto.NewPassword);
            await UnitOfWork.Users.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto?> AssignRolesToUserAsync(Guid userId, AssignRolesDto dto)
        {
            var user = await UnitOfWork.Users.GetByIdAsync(userId);
            if (user == null) return null;

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                var allUserRoles = await UnitOfWork.Users.GetAllUserRolesAsync(userId);

                var requestedRoleIds = dto.RoleIds.ToHashSet();
                var currentActiveRoleIds = allUserRoles
                    .Where(ur => !ur.IsDeleted)
                    .Select(ur => ur.RoleId)
                    .ToHashSet();

                // Remove roles not in the new list (soft-delete via repo)
                foreach (var userRole in allUserRoles.Where(ur => !ur.IsDeleted))
                {
                    if (!requestedRoleIds.Contains(userRole.RoleId))
                    {
                        await UnitOfWork.Users.DeleteUserRoleAsync(userRole);
                    }
                }

                // Add new roles or reactivate soft-deleted ones
                foreach (var roleId in requestedRoleIds)
                {
                    if (!currentActiveRoleIds.Contains(roleId))
                    {
                        var existingDeleted = allUserRoles
                            .FirstOrDefault(ur => ur.RoleId == roleId && ur.IsDeleted);

                        if (existingDeleted != null)
                        {
                            existingDeleted.IsDeleted = false;
                            existingDeleted.UpdatedAt = DateTime.UtcNow;
                        }
                        else
                        {
                            var role = await UnitOfWork.Roles.GetByIdAsync(roleId);
                            if (role != null)
                            {
                                await UnitOfWork.Users.AddUserRoleAsync(new UserRole
                                {
                                    Id = Guid.NewGuid(),
                                    UserId = userId,
                                    RoleId = roleId
                                });
                            }
                        }
                    }
                }

                await UnitOfWork.SaveChangesAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }

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
