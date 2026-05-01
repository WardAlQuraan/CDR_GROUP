using Microsoft.AspNetCore.Http;
using cdr_group.Contracts.DTOs.CompanyBranch;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyBranchService : IBaseService<CompanyBranchDto, CreateCompanyBranchDto, UpdateCompanyBranchDto>
    {
        Task<IEnumerable<CompanyBranchDto>> GetByCompanyIdAsync(Guid companyId);
        Task<string> UploadImageAsync(Guid id, IFormFile file);
    }
}
