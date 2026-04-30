using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyPreContractStudy;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyPreContractStudyService : BaseService<CompanyPreContractStudy, CompanyPreContractStudyDto, CreateCompanyPreContractStudyDto, UpdateCompanyPreContractStudyDto>, ICompanyPreContractStudyService
    {
        public CompanyPreContractStudyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyPreContractStudy> Repository => UnitOfWork.CompanyPreContractStudies;

        public override async Task<PagedResult<CompanyPreContractStudyDto>> GetPagedAsync(PagedRequest request)
        {
            var studyRequest = request as CompanyPreContractStudyPagedRequest ?? new CompanyPreContractStudyPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (studies, totalCount) = await UnitOfWork.CompanyPreContractStudies.GetCompanyPreContractStudiesPagedAsync(studyRequest);
            var dtos = Mapper.Map<List<CompanyPreContractStudyDto>>(studies);
            return new PagedResult<CompanyPreContractStudyDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyPreContractStudyDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyPreContractStudies.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyPreContractStudyDto>(entity);
        }

        public async Task<IEnumerable<CompanyPreContractStudyDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var studies = await UnitOfWork.CompanyPreContractStudies.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyPreContractStudyDto>>(studies);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyPreContractStudyDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyPreContractStudyDto dto, CompanyPreContractStudy entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }
    }
}
