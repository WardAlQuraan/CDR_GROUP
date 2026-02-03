using cdr_group.Contracts.DTOs.Department;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IDepartmentService : IBaseService<DepartmentDto, CreateDepartmentDto, UpdateDepartmentDto>
    {
        Task<DepartmentDto?> GetByCodeAsync(string code);
        Task<DepartmentDto?> GetByNameAsync(string name);
        Task<DepartmentWithSubDepartmentsDto?> GetWithSubDepartmentsAsync(Guid id);
        Task<IEnumerable<DepartmentBasicDto>> GetSubDepartmentsAsync(Guid parentId);
        Task<IEnumerable<DepartmentDto>> GetRootDepartmentsAsync();
        Task<IEnumerable<DepartmentDto>> GetActiveDepartmentsAsync();
        Task<DepartmentDto?> AssignManagerAsync(Guid departmentId, Guid? managerId);
    }
}
