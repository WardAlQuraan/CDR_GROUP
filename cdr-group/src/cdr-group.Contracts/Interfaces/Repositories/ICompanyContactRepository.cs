using cdr_group.Contracts.DTOs.CompanyContact;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyContactRepository : IRepository<CompanyContact>
    {
        Task<CompanyContact?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyContact>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyContact> Items, int TotalCount)> GetCompanyContactsPagedAsync(CompanyContactPagedRequest request);
    }
}
