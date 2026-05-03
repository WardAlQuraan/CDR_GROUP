using Microsoft.AspNetCore.Http;
using cdr_group.Contracts.DTOs.CompanyClientReach;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyClientReachService : IBaseService<CompanyClientReachDto, CreateCompanyClientReachDto, UpdateCompanyClientReachDto>
    {
        Task<IEnumerable<CompanyClientReachDto>> GetByCompanyIdAsync(Guid companyId);
        Task<string> UploadLogoAsync(Guid id, IFormFile file);
    }
}
