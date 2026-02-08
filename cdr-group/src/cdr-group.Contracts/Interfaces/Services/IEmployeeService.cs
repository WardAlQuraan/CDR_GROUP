using cdr_group.Contracts.DTOs.Employee;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<EmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>
    {
        Task<EmployeeDto?> GetByEmployeeCodeAsync(string employeeCode);
        Task<EmployeeDto?> GetByUserIdAsync(Guid userId);
        Task<EmployeeWithSubordinatesDto?> GetWithSubordinatesAsync(Guid id);
        Task<IEnumerable<EmployeeBasicDto>> GetSubordinatesAsync(Guid managerId);
        Task<IEnumerable<EmployeeDto>> GetByDepartmentIdAsync(Guid departmentId);
        Task<EmployeeDto?> AssignManagerAsync(Guid employeeId, Guid? managerId);
        Task<EmployeeDto?> AssignDepartmentAsync(Guid employeeId, Guid? departmentId);
        Task<EmployeeDto?> LinkToUserAsync(Guid employeeId, Guid? userId);
        Task<IEnumerable<EmployeeTreeNodeDto>> GetTreeAsync(GetTreeRequest request);
    }
}
