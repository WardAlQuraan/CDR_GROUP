using cdr_group.Contracts.DTOs.Company;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyService : IBaseService<CompanyDto, CreateCompanyDto, UpdateCompanyDto>
    {
        Task<IEnumerable<CompanyDto>> GetActiveCompaniesAsync();
        Task<IEnumerable<CompanyDto>> GetTreeAsync();
    }
}
