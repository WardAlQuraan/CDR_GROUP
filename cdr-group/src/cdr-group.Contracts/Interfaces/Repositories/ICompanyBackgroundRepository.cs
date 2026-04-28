using cdr_group.Contracts.DTOs.CompanyBackground;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyBackgroundRepository : IRepository<CompanyBackground>
    {
        Task<CompanyBackground?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyBackground>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyBackground> Items, int TotalCount)> GetCompanyBackgroundsPagedAsync(CompanyBackgroundPagedRequest request);
    }
}
