using AutoMapper;
using cdr_group.Contracts.DTOs.Branch;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class BranchService : BaseService<Branch, BranchDto, CreateBranchDto, UpdateBranchDto>, IBranchService
    {
        public BranchService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Branch> Repository => UnitOfWork.Branches;

        public override async Task<IEnumerable<BranchDto>> GetAllAsync()
        {
            var (branches, _) = await UnitOfWork.Branches.GetBranchesPagedAsync(new PagedRequest { PageSize = int.MaxValue });
            return Mapper.Map<IEnumerable<BranchDto>>(branches);
        }

        public override async Task<PagedResult<BranchDto>> GetPagedAsync(PagedRequest request)
        {
            var (branches, totalCount) = await UnitOfWork.Branches.GetBranchesPagedAsync(request);
            var branchDtos = Mapper.Map<List<BranchDto>>(branches);
            return new PagedResult<BranchDto>(branchDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<BranchDto?> GetByCodeAsync(string code)
        {
            var branch = await UnitOfWork.Branches.GetByCodeAsync(code);
            return Mapper.Map<BranchDto>(branch);
        }

        public async Task<IEnumerable<BranchDto>> GetActiveAsync()
        {
            var branches = await UnitOfWork.Branches.GetActiveAsync();
            return Mapper.Map<IEnumerable<BranchDto>>(branches);
        }

        public async Task<IEnumerable<BranchDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var branches = await UnitOfWork.Branches.GetByCompanyIdAsync(companyId);
            return Mapper.Map<IEnumerable<BranchDto>>(branches);
        }

        protected override async Task ValidateCreateAsync(CreateBranchDto dto)
        {
            if (await UnitOfWork.Branches.BranchCodeExistsAsync(dto.Code))
            {
                throw new InvalidOperationException(Messages.BranchCodeExists);
            }

            await ValidateCompanyExists(dto.CompanyId);
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateBranchDto dto, Branch entity)
        {
            if (dto.Code != null && dto.Code != entity.Code)
            {
                if (await UnitOfWork.Branches.BranchCodeExistsAsync(dto.Code, id))
                {
                    throw new InvalidOperationException(Messages.BranchCodeExists);
                }
            }

            if (dto.CompanyId.HasValue)
            {
                await ValidateCompanyExists(dto.CompanyId.Value);
            }
        }

        private async Task ValidateCompanyExists(Guid companyId)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(companyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }
    }
}
