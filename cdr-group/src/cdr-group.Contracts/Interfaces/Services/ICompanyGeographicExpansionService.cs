using cdr_group.Contracts.DTOs.CompanyGeographicExpansion;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyGeographicExpansionService : IBaseService<CompanyGeographicExpansionDto, CreateCompanyGeographicExpansionDto, UpdateCompanyGeographicExpansionDto>
    {
        Task<IEnumerable<CompanyGeographicExpansionDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
