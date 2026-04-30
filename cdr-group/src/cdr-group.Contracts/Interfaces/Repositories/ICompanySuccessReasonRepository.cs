using cdr_group.Contracts.DTOs.CompanySuccessReason;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanySuccessReasonRepository : IRepository<CompanySuccessReason>
    {
        Task<CompanySuccessReason?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanySuccessReason>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanySuccessReason> Items, int TotalCount)> GetCompanySuccessReasonsPagedAsync(CompanySuccessReasonPagedRequest request);
    }
}
