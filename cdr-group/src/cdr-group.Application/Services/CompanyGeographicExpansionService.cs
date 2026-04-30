using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyGeographicExpansion;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyGeographicExpansionService : BaseService<CompanyGeographicExpansion, CompanyGeographicExpansionDto, CreateCompanyGeographicExpansionDto, UpdateCompanyGeographicExpansionDto>, ICompanyGeographicExpansionService
    {
        public CompanyGeographicExpansionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyGeographicExpansion> Repository => UnitOfWork.CompanyGeographicExpansions;

        public override async Task<PagedResult<CompanyGeographicExpansionDto>> GetPagedAsync(PagedRequest request)
        {
            var expansionRequest = request as CompanyGeographicExpansionPagedRequest ?? new CompanyGeographicExpansionPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (expansions, totalCount) = await UnitOfWork.CompanyGeographicExpansions.GetCompanyGeographicExpansionsPagedAsync(expansionRequest);
            var dtos = Mapper.Map<List<CompanyGeographicExpansionDto>>(expansions);
            return new PagedResult<CompanyGeographicExpansionDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyGeographicExpansionDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyGeographicExpansions.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyGeographicExpansionDto>(entity);
        }

        public async Task<IEnumerable<CompanyGeographicExpansionDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var expansions = await UnitOfWork.CompanyGeographicExpansions.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyGeographicExpansionDto>>(expansions);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyGeographicExpansionDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyGeographicExpansionDto dto, CompanyGeographicExpansion entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }
    }
}
