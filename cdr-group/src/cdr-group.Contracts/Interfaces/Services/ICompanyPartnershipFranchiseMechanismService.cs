using cdr_group.Contracts.DTOs.CompanyPartnershipFranchiseMechanism;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyPartnershipFranchiseMechanismService : IBaseService<CompanyPartnershipFranchiseMechanismDto, CreateCompanyPartnershipFranchiseMechanismDto, UpdateCompanyPartnershipFranchiseMechanismDto>
    {
        Task<IEnumerable<CompanyPartnershipFranchiseMechanismDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
