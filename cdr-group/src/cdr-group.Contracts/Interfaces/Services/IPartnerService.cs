using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Partner;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IPartnerService : IBaseService<PartnerDto, CreatePartnerDto, UpdatePartnerDto>
    {
        Task<PagedResult<PartnerDto>> GetPartnersPagedAsync(PartnerPagedRequest request);
    }
}
