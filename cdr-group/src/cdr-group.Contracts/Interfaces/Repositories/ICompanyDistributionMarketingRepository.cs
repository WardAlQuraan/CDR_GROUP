using cdr_group.Contracts.DTOs.CompanyDistributionMarketing;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyDistributionMarketingRepository : IRepository<CompanyDistributionMarketing>
    {
        Task<CompanyDistributionMarketing?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyDistributionMarketing>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyDistributionMarketing> Items, int TotalCount)> GetCompanyDistributionMarketingsPagedAsync(CompanyDistributionMarketingPagedRequest request);
    }
}
