using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Event;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event?> GetWithCompanyAsync(Guid id);
        Task<IEnumerable<Event>> GetByCompanyIdAsync(Guid companyId);
        Task<(IEnumerable<Event> Items, int TotalCount)> GetEventsPagedAsync(EventPagedRequest request);
    }
}
