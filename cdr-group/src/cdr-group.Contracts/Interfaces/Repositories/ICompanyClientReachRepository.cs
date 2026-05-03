using cdr_group.Contracts.DTOs.CompanyClientReach;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyClientReachRepository : IRepository<CompanyClientReach>
    {
        Task<CompanyClientReach?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyClientReach>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyClientReach> Items, int TotalCount)> GetCompanyClientReachesPagedAsync(CompanyClientReachPagedRequest request);
    }
}
