using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Event;
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

        public async Task<Event?> GetWithCompanyAsync(Guid id)
        {
            return await _dbSet
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<Event>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _dbSet
                .Include(e => e.Company)
                .Where(e => e.CompanyId == companyId && !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Event> Items, int TotalCount)> GetEventsPagedAsync(EventPagedRequest request)
        {
            var query = _dbSet
                .Include(e => e.Company)
                .Where(e => !e.IsDeleted);

            if (request.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == request.CompanyId.Value);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
