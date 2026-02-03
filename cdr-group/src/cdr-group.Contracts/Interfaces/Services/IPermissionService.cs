using cdr_group.Contracts.DTOs.Identity;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IPermissionService : IBaseService<PermissionDto, CreatePermissionDto, UpdatePermissionDto>
    {
        Task<PermissionDto?> GetByNameAsync(string name);
        Task<IEnumerable<PermissionDto>> GetByModuleAsync(string module);
        Task<IEnumerable<PermissionDto>> GetByRoleIdAsync(Guid roleId);
        Task<IEnumerable<PermissionDto>> GetByUserIdAsync(Guid userId);
    }
}
