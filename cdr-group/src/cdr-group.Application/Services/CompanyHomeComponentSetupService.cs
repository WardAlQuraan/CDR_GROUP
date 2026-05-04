using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyHomeComponentSetup;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyHomeComponentSetupService : BaseService<CompanyHomeComponentSetup, CompanyHomeComponentSetupDto, CreateCompanyHomeComponentSetupDto, UpdateCompanyHomeComponentSetupDto>, ICompanyHomeComponentSetupService
    {
        public CompanyHomeComponentSetupService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyHomeComponentSetup> Repository => UnitOfWork.CompanyHomeComponentSetups;

        public override async Task<PagedResult<CompanyHomeComponentSetupDto>> GetPagedAsync(PagedRequest request)
        {
            var pagedRequest = request as CompanyHomeComponentSetupPagedRequest ?? new CompanyHomeComponentSetupPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (items, totalCount) = await UnitOfWork.CompanyHomeComponentSetups.GetCompanyHomeComponentSetupsPagedAsync(pagedRequest);
            var dtos = Mapper.Map<List<CompanyHomeComponentSetupDto>>(items);
            return new PagedResult<CompanyHomeComponentSetupDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyHomeComponentSetupDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyHomeComponentSetups.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyHomeComponentSetupDto>(entity);
        }

        public async Task<IEnumerable<CompanyHomeComponentSetupDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var items = await UnitOfWork.CompanyHomeComponentSetups.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyHomeComponentSetupDto>>(items);
        }

        public async Task<CompanyHomeComponentSetupDto?> GetByCompanyAndComponentCodeAsync(Guid companyId, string componentCode)
        {
            var entity = await UnitOfWork.CompanyHomeComponentSetups.GetByCompanyAndComponentCodeAsync(companyId, componentCode);
            return Mapper.Map<CompanyHomeComponentSetupDto>(entity);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyHomeComponentSetupDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }

            var existing = await UnitOfWork.CompanyHomeComponentSetups.GetByCompanyAndComponentCodeAsync(dto.CompanyId, dto.ComponentCode);
            if (existing != null)
            {
                throw new InvalidOperationException($"A component setup with code '{dto.ComponentCode}' already exists for this company.");
            }
        }

        protected override Task ValidateUpdateAsync(Guid id, UpdateCompanyHomeComponentSetupDto dto, CompanyHomeComponentSetup entity)
        {
            return Task.CompletedTask;
        }
    }
}
