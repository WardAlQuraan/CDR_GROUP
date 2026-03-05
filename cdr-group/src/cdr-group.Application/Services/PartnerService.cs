using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Partner;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class PartnerService : BaseService<Partner, PartnerDto, CreatePartnerDto, UpdatePartnerDto>, IPartnerService
    {
        public PartnerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Partner> Repository => UnitOfWork.Partners;

        public override async Task<PagedResult<PartnerDto>> GetPagedAsync(PagedRequest request)
        {
            var partnerRequest = request as PartnerPagedRequest ?? new PartnerPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };
            var (items, totalCount) = await UnitOfWork.Partners.GetPartnersPagedAsync(partnerRequest);
            var dtos = Mapper.Map<List<PartnerDto>>(items);
            return new PagedResult<PartnerDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<PagedResult<PartnerDto>> GetPartnersPagedAsync(PartnerPagedRequest request)
        {
            var (items, totalCount) = await UnitOfWork.Partners.GetPartnersPagedAsync(request);
            var dtos = Mapper.Map<List<PartnerDto>>(items);
            return new PagedResult<PartnerDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
