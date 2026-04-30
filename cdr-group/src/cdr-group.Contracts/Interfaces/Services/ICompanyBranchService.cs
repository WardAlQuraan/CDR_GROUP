using cdr_group.Contracts.DTOs.CompanyBranch;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyBranchService : IBaseService<CompanyBranchDto, CreateCompanyBranchDto, UpdateCompanyBranchDto>
    {
        Task<IEnumerable<CompanyBranchDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
