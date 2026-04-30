using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanySuccessReason;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanySuccessReasonService : BaseService<CompanySuccessReason, CompanySuccessReasonDto, CreateCompanySuccessReasonDto, UpdateCompanySuccessReasonDto>, ICompanySuccessReasonService
    {
        public CompanySuccessReasonService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanySuccessReason> Repository => UnitOfWork.CompanySuccessReasons;

        public override async Task<PagedResult<CompanySuccessReasonDto>> GetPagedAsync(PagedRequest request)
        {
            var reasonRequest = request as CompanySuccessReasonPagedRequest ?? new CompanySuccessReasonPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (reasons, totalCount) = await UnitOfWork.CompanySuccessReasons.GetCompanySuccessReasonsPagedAsync(reasonRequest);
            var dtos = Mapper.Map<List<CompanySuccessReasonDto>>(reasons);
            return new PagedResult<CompanySuccessReasonDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanySuccessReasonDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanySuccessReasons.GetWithCompanyAsync(id);
            return Mapper.Map<CompanySuccessReasonDto>(entity);
        }

        public async Task<IEnumerable<CompanySuccessReasonDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var reasons = await UnitOfWork.CompanySuccessReasons.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanySuccessReasonDto>>(reasons);
        }

        protected override async Task ValidateCreateAsync(CreateCompanySuccessReasonDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanySuccessReasonDto dto, CompanySuccessReason entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }
    }
}
