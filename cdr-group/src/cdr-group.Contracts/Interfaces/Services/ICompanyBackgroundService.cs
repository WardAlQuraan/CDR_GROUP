using cdr_group.Contracts.DTOs.CompanyBackground;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyBackgroundService : IBaseService<CompanyBackgroundDto, CreateCompanyBackgroundDto, UpdateCompanyBackgroundDto>
    {
        Task<IEnumerable<CompanyBackgroundDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
