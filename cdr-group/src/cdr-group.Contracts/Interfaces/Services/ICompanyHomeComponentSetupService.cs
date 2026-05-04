using cdr_group.Contracts.DTOs.CompanyHomeComponentSetup;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyHomeComponentSetupService : IBaseService<CompanyHomeComponentSetupDto, CreateCompanyHomeComponentSetupDto, UpdateCompanyHomeComponentSetupDto>
    {
        Task<IEnumerable<CompanyHomeComponentSetupDto>> GetByCompanyIdAsync(Guid companyId);
        Task<CompanyHomeComponentSetupDto?> GetByCompanyAndComponentCodeAsync(Guid companyId, string componentCode);
    }
}
