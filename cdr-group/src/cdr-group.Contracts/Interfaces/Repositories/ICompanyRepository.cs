using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company?> GetByCodeAsync(string code);
        Task<IEnumerable<Company>> GetActiveCompaniesAsync();
        Task<(IEnumerable<Company> Items, int TotalCount)> GetCompaniesPagedAsync(PagedRequest request);
        Task<bool> CompanyCodeExistsAsync(string code, Guid? excludeId = null);
        Task<bool> HasEmployeesAsync(Guid companyId);
    }
}
