using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities.Identity;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission?> GetByNameAsync(string name);
        Task<IEnumerable<Permission>> GetByModuleAsync(string module);
        Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId);
        Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(Guid userId);
        Task<(IEnumerable<Permission> Permissions, int TotalCount)> GetPermissionsPagedAsync(PagedRequest request);
        Task<bool> NameExistsAsync(string name);
    }
}
