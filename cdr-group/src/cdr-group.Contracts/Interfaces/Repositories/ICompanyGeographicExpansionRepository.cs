using cdr_group.Contracts.DTOs.CompanyGeographicExpansion;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyGeographicExpansionRepository : IRepository<CompanyGeographicExpansion>
    {
        Task<CompanyGeographicExpansion?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyGeographicExpansion>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyGeographicExpansion> Items, int TotalCount)> GetCompanyGeographicExpansionsPagedAsync(CompanyGeographicExpansionPagedRequest request);
    }
}
