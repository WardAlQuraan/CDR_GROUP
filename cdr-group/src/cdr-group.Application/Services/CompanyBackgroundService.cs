using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyBackground;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyBackgroundService : BaseService<CompanyBackground, CompanyBackgroundDto, CreateCompanyBackgroundDto, UpdateCompanyBackgroundDto>, ICompanyBackgroundService
    {
        public CompanyBackgroundService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyBackground> Repository => UnitOfWork.CompanyBackgrounds;

        public override async Task<PagedResult<CompanyBackgroundDto>> GetPagedAsync(PagedRequest request)
        {
            var backgroundRequest = request as CompanyBackgroundPagedRequest ?? new CompanyBackgroundPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (backgrounds, totalCount) = await UnitOfWork.CompanyBackgrounds.GetCompanyBackgroundsPagedAsync(backgroundRequest);
            var dtos = Mapper.Map<List<CompanyBackgroundDto>>(backgrounds);
            return new PagedResult<CompanyBackgroundDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyBackgroundDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyBackgrounds.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyBackgroundDto>(entity);
        }

        public async Task<IEnumerable<CompanyBackgroundDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var backgrounds = await UnitOfWork.CompanyBackgrounds.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyBackgroundDto>>(backgrounds);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyBackgroundDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyBackgroundDto dto, CompanyBackground entity)
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
