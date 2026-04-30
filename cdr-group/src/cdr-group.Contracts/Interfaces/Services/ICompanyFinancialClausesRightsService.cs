using cdr_group.Contracts.DTOs.CompanyFinancialClausesRights;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyFinancialClausesRightsService : IBaseService<CompanyFinancialClausesRightsDto, CreateCompanyFinancialClausesRightsDto, UpdateCompanyFinancialClausesRightsDto>
    {
        Task<IEnumerable<CompanyFinancialClausesRightsDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
