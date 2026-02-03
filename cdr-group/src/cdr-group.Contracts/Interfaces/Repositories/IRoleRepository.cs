using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities.Identity;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name);
        Task<Role?> GetWithPermissionsAsync(Guid id);
        Task<Role?> GetWithAllPermissionsAsync(Guid id);
        Task<IEnumerable<Role>> GetRolesWithPermissionsAsync();
        Task<(IEnumerable<Role> Roles, int TotalCount)> GetRolesWithPermissionsPagedAsync(PagedRequest request);
        Task<bool> NameExistsAsync(string name);
        Task<List<RolePermission>> GetAllRolePermissionsAsync(Guid roleId);
        Task AddRolePermissionAsync(RolePermission rolePermission);
        Task UpdateRolePermissionAsync(RolePermission rolePermission);
    }
}
