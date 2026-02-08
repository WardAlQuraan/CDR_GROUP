using cdr_group.Contracts.DTOs.Company;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyService : IBaseService<CompanyDto, CreateCompanyDto, UpdateCompanyDto>
    {
        Task<CompanyDto?> GetByCodeAsync(string code);
        Task<IEnumerable<CompanyDto>> GetActiveCompaniesAsync();
    }
}
