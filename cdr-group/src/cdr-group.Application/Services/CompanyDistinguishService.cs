using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyDistinguish;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyDistinguishService : BaseService<CompanyDistinguish, CompanyDistinguishDto, CreateCompanyDistinguishDto, UpdateCompanyDistinguishDto>, ICompanyDistinguishService
    {
        public CompanyDistinguishService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyDistinguish> Repository => UnitOfWork.CompanyDistinguishes;

        public override async Task<PagedResult<CompanyDistinguishDto>> GetPagedAsync(PagedRequest request)
        {
            var distinguishRequest = request as CompanyDistinguishPagedRequest ?? new CompanyDistinguishPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (distinguishes, totalCount) = await UnitOfWork.CompanyDistinguishes.GetCompanyDistinguishesPagedAsync(distinguishRequest);
            var dtos = Mapper.Map<List<CompanyDistinguishDto>>(distinguishes);
            return new PagedResult<CompanyDistinguishDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyDistinguishDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyDistinguishes.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyDistinguishDto>(entity);
        }

        public async Task<IEnumerable<CompanyDistinguishDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var distinguishes = await UnitOfWork.CompanyDistinguishes.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyDistinguishDto>>(distinguishes);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyDistinguishDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyDistinguishDto dto, CompanyDistinguish entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }
    }
}
