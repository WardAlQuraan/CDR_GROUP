using cdr_group.Contracts.DTOs.CompanyFinancialClausesRights;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyFinancialClausesRightsRepository : IRepository<CompanyFinancialClausesRights>
    {
        Task<CompanyFinancialClausesRights?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyFinancialClausesRights>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyFinancialClausesRights> Items, int TotalCount)> GetCompanyFinancialClausesRightsPagedAsync(CompanyFinancialClausesRightsPagedRequest request);
    }
}
