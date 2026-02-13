using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ISalaryHistoryRepository : IRepository<SalaryHistory>
    {
        Task<SalaryHistory?> GetWithEmployeeAsync(Guid id);
        Task<IEnumerable<SalaryHistory>> GetByEmployeeIdAsync(Guid employeeId);
        Task<(IEnumerable<SalaryHistory> Items, int TotalCount)> GetSalaryHistoriesPagedAsync(PagedRequest request, Guid? employeeId = null);
    }
}
