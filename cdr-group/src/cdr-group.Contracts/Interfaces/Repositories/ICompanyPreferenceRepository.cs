using cdr_group.Contracts.DTOs.CompanyPreference;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyPreferenceRepository : IRepository<CompanyPreference>
    {
        Task<CompanyPreference?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyPreference>> GetByCompanyIdAsync(Guid companyId);
        Task<CompanyPreference?> GetByCompanyAndCodeAsync(Guid companyId, string code);
        Task<(IEnumerable<CompanyPreference> Items, int TotalCount)> GetCompanyPreferencesPagedAsync(CompanyPreferencePagedRequest request);
    }
}
