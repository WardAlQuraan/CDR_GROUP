using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IAuditLogRepository
    {
        Task<(IEnumerable<AuditLog> Items, int TotalCount)> GetPagedAsync(PagedRequest request, string? entityName = null, string? entityId = null);
    }
}
