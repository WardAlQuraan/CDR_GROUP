using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<AuditLog> Items, int TotalCount)> GetPagedAsync(PagedRequest request, string? entityName = null, string? entityId = null)
        {
            var query = _context.AuditLogs.AsQueryable().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(entityName))
            {
                query = query.Where(a => a.EntityName == entityName);
            }

            if (!string.IsNullOrWhiteSpace(entityId))
            {
                query = query.Where(a => a.EntityId == entityId);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, a => a.Timestamp);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);
        }
    }
}
