using cdr_group.Contracts.DTOs.CompanyHomeComponentSetup;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyHomeComponentSetupRepository : IRepository<CompanyHomeComponentSetup>
    {
        Task<CompanyHomeComponentSetup?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyHomeComponentSetup>> GetByCompanyIdAsync(Guid companyId);
        Task<CompanyHomeComponentSetup?> GetByCompanyAndComponentCodeAsync(Guid companyId, string componentCode);
        Task<(IEnumerable<CompanyHomeComponentSetup> Items, int TotalCount)> GetCompanyHomeComponentSetupsPagedAsync(CompanyHomeComponentSetupPagedRequest request);
    }
}
