using cdr_group.Contracts.DTOs.CompanyPartnershipFranchiseMechanism;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICompanyPartnershipFranchiseMechanismRepository : IRepository<CompanyPartnershipFranchiseMechanism>
    {
        Task<CompanyPartnershipFranchiseMechanism?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<CompanyPartnershipFranchiseMechanism>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<CompanyPartnershipFranchiseMechanism> Items, int TotalCount)> GetCompanyPartnershipFranchiseMechanismsPagedAsync(CompanyPartnershipFranchiseMechanismPagedRequest request);
    }
}
