using cdr_group.Contracts.DTOs.CompanySuccessReason;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanySuccessReasonService : IBaseService<CompanySuccessReasonDto, CreateCompanySuccessReasonDto, UpdateCompanySuccessReasonDto>
    {
        Task<IEnumerable<CompanySuccessReasonDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
