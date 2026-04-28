using cdr_group.Contracts.DTOs.CompanyForm;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyFormService : IBaseService<CompanyFormDto, CreateCompanyFormDto, UpdateCompanyFormDto>
    {
        Task<IEnumerable<CompanyFormDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
