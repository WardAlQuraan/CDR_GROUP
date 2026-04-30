using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyDistributionMarketing;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyDistributionMarketingService : BaseService<CompanyDistributionMarketing, CompanyDistributionMarketingDto, CreateCompanyDistributionMarketingDto, UpdateCompanyDistributionMarketingDto>, ICompanyDistributionMarketingService
    {
        public CompanyDistributionMarketingService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyDistributionMarketing> Repository => UnitOfWork.CompanyDistributionMarketings;

        public override async Task<PagedResult<CompanyDistributionMarketingDto>> GetPagedAsync(PagedRequest request)
        {
            var marketingRequest = request as CompanyDistributionMarketingPagedRequest ?? new CompanyDistributionMarketingPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (marketings, totalCount) = await UnitOfWork.CompanyDistributionMarketings.GetCompanyDistributionMarketingsPagedAsync(marketingRequest);
            var dtos = Mapper.Map<List<CompanyDistributionMarketingDto>>(marketings);
            return new PagedResult<CompanyDistributionMarketingDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyDistributionMarketingDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyDistributionMarketings.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyDistributionMarketingDto>(entity);
        }

        public async Task<IEnumerable<CompanyDistributionMarketingDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var marketings = await UnitOfWork.CompanyDistributionMarketings.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyDistributionMarketingDto>>(marketings);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyDistributionMarketingDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyDistributionMarketingDto dto, CompanyDistributionMarketing entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }
    }
}
