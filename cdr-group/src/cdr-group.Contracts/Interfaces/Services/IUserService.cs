using cdr_group.Contracts.DTOs.Identity;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IUserService : IBaseService<UserDto, CreateUserDto, UpdateUserDto>
    {
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<UserDto?> GetByEmailAsync(string email);
        Task<bool> ChangePasswordAsync(Guid id, ChangePasswordDto dto);
        Task<UserDto?> AssignRolesToUserAsync(Guid userId, AssignRolesDto dto);
        Task<UserDto?> RemoveRolesFromUserAsync(Guid userId, List<Guid> roleIds);
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<bool> ActivateUserAsync(Guid id);
        Task<bool> DeactivateUserAsync(Guid id);
    }
}
