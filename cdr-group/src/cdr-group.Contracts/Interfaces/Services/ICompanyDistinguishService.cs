using cdr_group.Contracts.DTOs.CompanyDistinguish;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyDistinguishService : IBaseService<CompanyDistinguishDto, CreateCompanyDistinguishDto, UpdateCompanyDistinguishDto>
    {
        Task<IEnumerable<CompanyDistinguishDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
