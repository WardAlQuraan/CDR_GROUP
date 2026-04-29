using cdr_group.Contracts.DTOs.CompanyPreference;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyPreferenceService : IBaseService<CompanyPreferenceDto, CreateCompanyPreferenceDto, UpdateCompanyPreferenceDto>
    {
        Task<IEnumerable<CompanyPreferenceDto>> GetByCompanyIdAsync(Guid companyId);
        Task<CompanyPreferenceDto?> GetByCompanyAndCodeAsync(Guid companyId, string code);
    }
}
