using cdr_group.Contracts.DTOs.CompanyPreContractStudy;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyPreContractStudyRepository : IRepository<CompanyPreContractStudy>
    {
        Task<CompanyPreContractStudy?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyPreContractStudy>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyPreContractStudy> Items, int TotalCount)> GetCompanyPreContractStudiesPagedAsync(CompanyPreContractStudyPagedRequest request);
    }
}
