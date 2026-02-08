using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IContactUsRepository : IRepository<ContactUs>
    {
        Task<(IEnumerable<ContactUs> Items, int TotalCount)> GetContactUsPagedAsync(PagedRequest request);
    }
}
