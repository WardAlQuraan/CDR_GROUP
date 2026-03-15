using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Employee;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetWithManagerAsync(Guid id);
        Task<Employee?> GetWithSubordinatesAsync(Guid id);
        Task<Employee?> GetByUserIdAsync(Guid userId, Guid? execludedId = null);
        Task<IEnumerable<Employee>> GetByManagerIdAsync(Guid managerId);
        Task<IEnumerable<Employee>> GetByCompanyIdAsync(Guid? companyId);
        Task<(IEnumerable<Employee> Items, int TotalCount)> GetEmployeesPagedAsync(EmployeePagedRequest request);

    }
}
