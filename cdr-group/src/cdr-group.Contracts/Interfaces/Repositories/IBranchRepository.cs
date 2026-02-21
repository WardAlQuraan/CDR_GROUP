using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<Branch?> GetByCodeAsync(string code);
        Task<IEnumerable<Branch>> GetActiveAsync();
        Task<(IEnumerable<Branch> Items, int TotalCount)> GetBranchesPagedAsync(PagedRequest request);
        Task<bool> BranchCodeExistsAsync(string code, Guid? excludeId = null);
        Task<IEnumerable<Branch>> GetByCompanyIdAsync(Guid companyId);
    }
}
