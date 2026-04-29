using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyPreference;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyPreferenceService : BaseService<CompanyPreference, CompanyPreferenceDto, CreateCompanyPreferenceDto, UpdateCompanyPreferenceDto>, ICompanyPreferenceService
    {
        public CompanyPreferenceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyPreference> Repository => UnitOfWork.CompanyPreferences;

        public override async Task<PagedResult<CompanyPreferenceDto>> GetPagedAsync(PagedRequest request)
        {
            var preferenceRequest = request as CompanyPreferencePagedRequest ?? new CompanyPreferencePagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (preferences, totalCount) = await UnitOfWork.CompanyPreferences.GetCompanyPreferencesPagedAsync(preferenceRequest);
            var dtos = Mapper.Map<List<CompanyPreferenceDto>>(preferences);
            return new PagedResult<CompanyPreferenceDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyPreferenceDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyPreferences.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyPreferenceDto>(entity);
        }

        public async Task<IEnumerable<CompanyPreferenceDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var preferences = await UnitOfWork.CompanyPreferences.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyPreferenceDto>>(preferences);
        }

        public async Task<CompanyPreferenceDto?> GetByCompanyAndCodeAsync(Guid companyId, string code)
        {
            var entity = await UnitOfWork.CompanyPreferences.GetByCompanyAndCodeAsync(companyId, code);
            return Mapper.Map<CompanyPreferenceDto>(entity);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyPreferenceDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }

            var existing = await UnitOfWork.CompanyPreferences.GetByCompanyAndCodeAsync(dto.CompanyId, dto.Code);
            if (existing != null)
            {
                throw new InvalidOperationException($"Preference '{dto.Code}' already exists for this company.");
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyPreferenceDto dto, CompanyPreference entity)
        {
            if (dto.CompanyId.HasValue)
            {
                if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
                {
                    throw new InvalidOperationException(Messages.CompanyNotFound);
                }
            }

            var targetCompanyId = dto.CompanyId ?? entity.CompanyId;
            var targetCode = dto.Code ?? entity.Code;
            if (targetCompanyId != entity.CompanyId || targetCode != entity.Code)
            {
                var existing = await UnitOfWork.CompanyPreferences.GetByCompanyAndCodeAsync(targetCompanyId, targetCode);
                if (existing != null && existing.Id != id)
                {
                    throw new InvalidOperationException($"Preference '{targetCode}' already exists for this company.");
                }
            }
        }
    }
}
