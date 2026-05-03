using cdr_group.Contracts.DTOs.CompanyTitleDescription;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyTitleDescriptionRepository : IRepository<CompanyTitleDescription>
    {
        Task<CompanyTitleDescription?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyTitleDescription>> GetByCompanyIdAsync(Guid companyId);
        Task<List<CompanyTitleDescription>> GetByCompanyAndCodeAsync(Guid companyId, string code);
        Task<(IEnumerable<CompanyTitleDescription> Items, int TotalCount)> GetCompanyTitleDescriptionsPagedAsync(CompanyTitleDescriptionPagedRequest request);
    }
}
