using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class FileAttachment : BaseEntity
    {
        /// <summary>
        /// Original file name
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Stored file name (unique name used in wwwroot)
        /// </summary>
        public string StoredFileName { get; set; } = string.Empty;

        /// <summary>
        /// Relative path from wwwroot (e.g., "uploads/employees/guid.pdf")
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// MIME content type (e.g., "application/pdf", "image/png")
        /// </summary>
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Reference to any entity (Employee, Department, etc.)
        /// </summary>
        public Guid? EntityId { get; set; }

        /// <summary>
        /// Entity type name for categorization (e.g., "Employee", "Department")
        /// </summary>
        public string? EntityType { get; set; }

        /// <summary>
        /// Optional description or label for the file
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Indicates if this is the primary file for the entity
        /// </summary>
        public bool IsPrimary { get; set; } = false;
    }
}
