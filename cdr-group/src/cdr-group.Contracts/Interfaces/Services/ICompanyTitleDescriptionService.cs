using cdr_group.Contracts.DTOs.CompanyTitleDescription;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyTitleDescriptionService : IBaseService<CompanyTitleDescriptionDto, CreateCompanyTitleDescriptionDto, UpdateCompanyTitleDescriptionDto>
    {
        Task<IEnumerable<CompanyTitleDescriptionDto>> GetByCompanyIdAsync(Guid companyId);
        Task<IEnumerable<CompanyTitleDescriptionDto>> GetByCompanyAndCodeAsync(Guid companyId, string code);
    }
}
