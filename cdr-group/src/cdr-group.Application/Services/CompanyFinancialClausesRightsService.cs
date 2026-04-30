using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyFinancialClausesRights;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyFinancialClausesRightsService : BaseService<CompanyFinancialClausesRights, CompanyFinancialClausesRightsDto, CreateCompanyFinancialClausesRightsDto, UpdateCompanyFinancialClausesRightsDto>, ICompanyFinancialClausesRightsService
    {
        public CompanyFinancialClausesRightsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyFinancialClausesRights> Repository => UnitOfWork.CompanyFinancialClausesRights;

        public override async Task<PagedResult<CompanyFinancialClausesRightsDto>> GetPagedAsync(PagedRequest request)
        {
            var clauseRequest = request as CompanyFinancialClausesRightsPagedRequest ?? new CompanyFinancialClausesRightsPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (clauses, totalCount) = await UnitOfWork.CompanyFinancialClausesRights.GetCompanyFinancialClausesRightsPagedAsync(clauseRequest);
            var dtos = Mapper.Map<List<CompanyFinancialClausesRightsDto>>(clauses);
            return new PagedResult<CompanyFinancialClausesRightsDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyFinancialClausesRightsDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyFinancialClausesRights.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyFinancialClausesRightsDto>(entity);
        }

        public async Task<IEnumerable<CompanyFinancialClausesRightsDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var clauses = await UnitOfWork.CompanyFinancialClausesRights.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyFinancialClausesRightsDto>>(clauses);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyFinancialClausesRightsDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyFinancialClausesRightsDto dto, CompanyFinancialClausesRights entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }
    }
}
