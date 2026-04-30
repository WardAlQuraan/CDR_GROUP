using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyPartnershipFranchiseMechanism;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyPartnershipFranchiseMechanismService : BaseService<CompanyPartnershipFranchiseMechanism, CompanyPartnershipFranchiseMechanismDto, CreateCompanyPartnershipFranchiseMechanismDto, UpdateCompanyPartnershipFranchiseMechanismDto>, ICompanyPartnershipFranchiseMechanismService
    {
        public CompanyPartnershipFranchiseMechanismService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyPartnershipFranchiseMechanism> Repository => UnitOfWork.CompanyPartnershipFranchiseMechanisms;

        public override async Task<PagedResult<CompanyPartnershipFranchiseMechanismDto>> GetPagedAsync(PagedRequest request)
        {
            var mechanismRequest = request as CompanyPartnershipFranchiseMechanismPagedRequest ?? new CompanyPartnershipFranchiseMechanismPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (mechanisms, totalCount) = await UnitOfWork.CompanyPartnershipFranchiseMechanisms.GetCompanyPartnershipFranchiseMechanismsPagedAsync(mechanismRequest);
            var dtos = Mapper.Map<List<CompanyPartnershipFranchiseMechanismDto>>(mechanisms);
            return new PagedResult<CompanyPartnershipFranchiseMechanismDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyPartnershipFranchiseMechanismDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyPartnershipFranchiseMechanisms.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyPartnershipFranchiseMechanismDto>(entity);
        }

        public async Task<IEnumerable<CompanyPartnershipFranchiseMechanismDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var mechanisms = await UnitOfWork.CompanyPartnershipFranchiseMechanisms.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyPartnershipFranchiseMechanismDto>>(mechanisms);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyPartnershipFranchiseMechanismDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyPartnershipFranchiseMechanismDto dto, CompanyPartnershipFranchiseMechanism entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }
    }
}
