using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.AuditLog
{
    public class AuditLogDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty;
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? OldDisplayValues { get; set; }
        public string? NewDisplayValues { get; set; }
        public string? AffectedColumns { get; set; }
        public string? PerformedBy { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
