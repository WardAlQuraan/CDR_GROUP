using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<IEnumerable<Company>> GetActiveCompaniesAsync();
        Task<(IEnumerable<Company> Items, int TotalCount)> GetCompaniesPagedAsync(PagedRequest request);
        Task<bool> HasEmployeesAsync(Guid companyId);
        Task<bool> HasChildrenAsync(Guid companyId);
        Task<bool> HasActiveChildrenAsync(Guid companyId);
        Task<Dictionary<Guid, int>> GetPartnersCountAsync(IEnumerable<Guid> companyIds);
        Task<IEnumerable<Company>> GetRelatedActiveCompaniesAsync(Guid? parentId = null);
    }
}
