using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using cdr_group.Domain.Entities;

namespace cdr_group.Persistence.Data
{
    internal class AuditEntry
    {
        public EntityEntry Entry { get; }
        public string EntityName { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty;
        public string? PerformedBy { get; set; }
        public DateTime Timestamp { get; set; }

        public Dictionary<string, object?> OldValues { get; } = new();
        public Dictionary<string, object?> NewValues { get; } = new();
        public List<string> AffectedColumns { get; } = new();
        public List<PropertyEntry> TemporaryProperties { get; } = new();

        public bool HasTemporaryProperties => TemporaryProperties.Count > 0;

        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public AuditLog ToAuditLog()
        {
            return new AuditLog
            {
                Id = Guid.NewGuid(),
                EntityName = EntityName,
                EntityId = Entry.Properties
                    .FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue?.ToString() ?? string.Empty,
                ActionType = ActionType,
                OldValues = OldValues.Count > 0
                    ? JsonSerializer.Serialize(OldValues, _jsonOptions)
                    : null,
                NewValues = NewValues.Count > 0
                    ? JsonSerializer.Serialize(NewValues, _jsonOptions)
                    : null,
                AffectedColumns = AffectedColumns.Count > 0
                    ? JsonSerializer.Serialize(AffectedColumns, _jsonOptions)
                    : null,
                PerformedBy = PerformedBy,
                Timestamp = Timestamp
            };
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
