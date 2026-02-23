using cdr_group.Contracts.DTOs.AuditLog;
using cdr_group.Contracts.DTOs.Common;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IAuditLogService
    {
        Task<PagedResult<AuditLogDto>> GetPagedAsync(PagedRequest request, string? entityName = null, string? entityId = null);
    }
}
