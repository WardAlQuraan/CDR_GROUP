using cdr_group.Contracts.DTOs.CompanyDistributionMarketing;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyDistributionMarketingService : IBaseService<CompanyDistributionMarketingDto, CreateCompanyDistributionMarketingDto, UpdateCompanyDistributionMarketingDto>
    {
        Task<IEnumerable<CompanyDistributionMarketingDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
