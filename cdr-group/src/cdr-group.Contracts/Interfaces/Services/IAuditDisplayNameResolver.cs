using cdr_group.Contracts.DTOs.AuditLog;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IAuditDisplayNameResolver
    {
        Task ResolveDisplayNamesAsync(List<AuditLogDto> dtos);
    }
}
