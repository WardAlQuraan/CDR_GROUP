using cdr_group.Contracts.DTOs.Partner;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        Task<(IEnumerable<Partner> Items, int TotalCount)> GetPartnersPagedAsync(PartnerPagedRequest request);
        Task<IEnumerable<Partner>> GetAllByCompanyIdAsync(Guid companyId);
    }
}
