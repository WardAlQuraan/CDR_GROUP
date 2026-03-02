using cdr_group.Contracts.DTOs.CompanyContact;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyContactService : IBaseService<CompanyContactDto, CreateCompanyContactDto, UpdateCompanyContactDto>
    {
        Task<IEnumerable<CompanyContactDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
