using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event?> GetWithDepartmentAsync(Guid id);
        Task<IEnumerable<Event>> GetByDepartmentIdAsync(Guid departmentId);
        Task<(IEnumerable<Event> Items, int TotalCount)> GetEventsPagedAsync(PagedRequest request);
    }
}
