using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyForm;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyFormService : BaseService<CompanyForm, CompanyFormDto, CreateCompanyFormDto, UpdateCompanyFormDto>, ICompanyFormService
    {
        public CompanyFormService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyForm> Repository => UnitOfWork.CompanyForms;

        public override async Task<PagedResult<CompanyFormDto>> GetPagedAsync(PagedRequest request)
        {
            var formRequest = request as CompanyFormPagedRequest ?? new CompanyFormPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (forms, totalCount) = await UnitOfWork.CompanyForms.GetCompanyFormsPagedAsync(formRequest);
            var dtos = Mapper.Map<List<CompanyFormDto>>(forms);
            return new PagedResult<CompanyFormDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyFormDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyForms.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyFormDto>(entity);
        }

        public async Task<IEnumerable<CompanyFormDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var forms = await UnitOfWork.CompanyForms.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyFormDto>>(forms);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyFormDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyFormDto dto, CompanyForm entity)
        {
            if (dto.CompanyId.HasValue)
            {
                if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
                {
                    throw new InvalidOperationException(Messages.CompanyNotFound);
                }
            }
        }
    }
}
