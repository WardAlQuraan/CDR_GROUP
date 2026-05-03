using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyTitleDescription;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyTitleDescriptionService : BaseService<CompanyTitleDescription, CompanyTitleDescriptionDto, CreateCompanyTitleDescriptionDto, UpdateCompanyTitleDescriptionDto>, ICompanyTitleDescriptionService
    {
        public CompanyTitleDescriptionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyTitleDescription> Repository => UnitOfWork.CompanyTitleDescriptions;

        public override async Task<PagedResult<CompanyTitleDescriptionDto>> GetPagedAsync(PagedRequest request)
        {
            var pagedRequest = request as CompanyTitleDescriptionPagedRequest ?? new CompanyTitleDescriptionPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (items, totalCount) = await UnitOfWork.CompanyTitleDescriptions.GetCompanyTitleDescriptionsPagedAsync(pagedRequest);
            var dtos = Mapper.Map<List<CompanyTitleDescriptionDto>>(items);
            return new PagedResult<CompanyTitleDescriptionDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyTitleDescriptionDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyTitleDescriptions.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyTitleDescriptionDto>(entity);
        }

        public async Task<IEnumerable<CompanyTitleDescriptionDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var items = await UnitOfWork.CompanyTitleDescriptions.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyTitleDescriptionDto>>(items);
        }

        public async Task<IEnumerable<CompanyTitleDescriptionDto>> GetByCompanyAndCodeAsync(Guid companyId, string code)
        {
            var entities = await UnitOfWork.CompanyTitleDescriptions.GetByCompanyAndCodeAsync(companyId, code);
            return Mapper.Map<List<CompanyTitleDescriptionDto>>(entities);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyTitleDescriptionDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }

           
        }

        protected override Task ValidateUpdateAsync(Guid id, UpdateCompanyTitleDescriptionDto dto, CompanyTitleDescription entity)
        {
            return Task.CompletedTask;
        }
    }
}
