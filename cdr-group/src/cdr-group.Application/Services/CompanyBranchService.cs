using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyBranch;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyBranchService : BaseService<CompanyBranch, CompanyBranchDto, CreateCompanyBranchDto, UpdateCompanyBranchDto>, ICompanyBranchService
    {
        public CompanyBranchService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyBranch> Repository => UnitOfWork.CompanyBranches;

        public override async Task<PagedResult<CompanyBranchDto>> GetPagedAsync(PagedRequest request)
        {
            var branchRequest = request as CompanyBranchPagedRequest ?? new CompanyBranchPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (branches, totalCount) = await UnitOfWork.CompanyBranches.GetCompanyBranchesPagedAsync(branchRequest);
            var dtos = Mapper.Map<List<CompanyBranchDto>>(branches);
            return new PagedResult<CompanyBranchDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyBranchDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyBranches.GetWithRelationsAsync(id);
            return Mapper.Map<CompanyBranchDto>(entity);
        }

        public async Task<IEnumerable<CompanyBranchDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var branches = await UnitOfWork.CompanyBranches.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyBranchDto>>(branches);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyBranchDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }

            if (!await UnitOfWork.Cities.ExistsAsync(dto.CityId))
            {
                throw new InvalidOperationException("City not found.");
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyBranchDto dto, CompanyBranch entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }

            if (dto.CityId.HasValue && !await UnitOfWork.Cities.ExistsAsync(dto.CityId.Value))
            {
                throw new InvalidOperationException("City not found.");
            }
        }
    }
}
