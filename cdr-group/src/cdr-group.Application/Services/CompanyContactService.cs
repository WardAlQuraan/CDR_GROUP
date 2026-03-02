using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyContact;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyContactService : BaseService<CompanyContact, CompanyContactDto, CreateCompanyContactDto, UpdateCompanyContactDto>, ICompanyContactService
    {
        public CompanyContactService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<CompanyContact> Repository => UnitOfWork.CompanyContacts;

        public override async Task<PagedResult<CompanyContactDto>> GetPagedAsync(PagedRequest request)
        {
            var contactRequest = request as CompanyContactPagedRequest ?? new CompanyContactPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (contacts, totalCount) = await UnitOfWork.CompanyContacts.GetCompanyContactsPagedAsync(contactRequest);
            var dtos = Mapper.Map<List<CompanyContactDto>>(contacts);
            return new PagedResult<CompanyContactDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyContactDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyContacts.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyContactDto>(entity);
        }

        public async Task<IEnumerable<CompanyContactDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var contacts = await UnitOfWork.CompanyContacts.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyContactDto>>(contacts);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyContactDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyContactDto dto, CompanyContact entity)
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
