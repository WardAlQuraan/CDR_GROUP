using cdr_group.Contracts.DTOs.CompanyPreContractStudy;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICompanyPreContractStudyService : IBaseService<CompanyPreContractStudyDto, CreateCompanyPreContractStudyDto, UpdateCompanyPreContractStudyDto>
    {
        Task<IEnumerable<CompanyPreContractStudyDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
