using cdr_group.Contracts.DTOs.CompanyDistinguish;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyDistinguishRepository : IRepository<CompanyDistinguish>
    {
        Task<CompanyDistinguish?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyDistinguish>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyDistinguish> Items, int TotalCount)> GetCompanyDistinguishesPagedAsync(CompanyDistinguishPagedRequest request);
    }
}
