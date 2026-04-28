using cdr_group.Contracts.DTOs.CompanyForm;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyFormRepository : IRepository<CompanyForm>
    {
        Task<CompanyForm?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyForm>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyForm> Items, int TotalCount)> GetCompanyFormsPagedAsync(CompanyFormPagedRequest request);
    }
}
