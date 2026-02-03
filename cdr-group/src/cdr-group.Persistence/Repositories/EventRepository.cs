using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Event?> GetWithDepartmentAsync(Guid id)
        {
            return await _dbSet
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<Event>> GetByDepartmentIdAsync(Guid departmentId)
        {
            return await _dbSet
                .Include(e => e.Department)
                .Where(e => e.DepartmentId == departmentId && !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Event> Items, int TotalCount)> GetEventsPagedAsync(PagedRequest request)
        {
            var query = _dbSet
                .Include(e => e.Department)
                .Where(e => !e.IsDeleted);

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
