using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department?> GetWithParentAsync(Guid id);
        Task<Department?> GetWithSubDepartmentsAsync(Guid id);
        Task<Department?> GetByCodeAsync(string code);
        Task<Department?> GetByNameAsync(string name);
        Task<IEnumerable<Department>> GetByParentIdAsync(Guid parentId);
        Task<IEnumerable<Department>> GetRootDepartmentsAsync();
        Task<IEnumerable<Department>> GetActiveDepartmentsAsync();
        Task<(IEnumerable<Department> Items, int TotalCount)> GetDepartmentsPagedAsync(PagedRequest request);
        Task<bool> DepartmentCodeExistsAsync(string code, Guid? excludeId = null);
        Task<bool> HasEmployeesAsync(Guid departmentId);
        Task<bool> HasSubDepartmentsAsync(Guid departmentId);
    }
}
