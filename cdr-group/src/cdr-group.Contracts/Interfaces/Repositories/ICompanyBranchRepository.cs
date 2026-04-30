using cdr_group.Contracts.DTOs.CompanyBranch;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyBranchRepository : IRepository<CompanyBranch>
    {
        Task<CompanyBranch?> GetWithRelationsAsync(Guid id);
        Task<IEnumerable<CompanyBranch>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyBranch> Items, int TotalCount)> GetCompanyBranchesPagedAsync(CompanyBranchPagedRequest request);
    }
}
