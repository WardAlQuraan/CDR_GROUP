using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IComplaintRepository : IRepository<Complaint>
    {
        Task<(IEnumerable<Complaint> Items, int TotalCount)> GetComplaintsPagedAsync(PagedRequest request);
    }
}
