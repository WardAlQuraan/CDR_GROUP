using cdr_group.Contracts.DTOs.Identity;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IRoleService : IBaseService<RoleDto, CreateRoleDto, UpdateRoleDto>
    {
        Task<RoleDto?> GetByNameAsync(string name);
        Task<RoleDto?> AssignPermissionsToRoleAsync(Guid roleId, AssignPermissionsDto dto);
        Task<RoleDto?> RemovePermissionsFromRoleAsync(Guid roleId, List<Guid> permissionIds);
    }
}
