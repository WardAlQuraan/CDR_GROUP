using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace cdr_group.Contracts.DTOs.FileAttachment
{
    public class FileAttachmentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string? FileUrl { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; }
        public Guid? EntityId { get; set; }
        public string? EntityType { get; set; }
        public string? Description { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class CreateFileAttachmentDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;

        public Guid? EntityId { get; set; }

        [StringLength(100)]
        public string? EntityType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// If true, removes all existing files for the same EntityId and EntityType before uploading
        /// </summary>
        public bool RemoveOldFiles { get; set; } = false;

        /// <summary>
        /// Indicates if this is the primary file for the entity
        /// </summary>
        public bool IsPrimary { get; set; } = false;
    }

    public class UpdateFileAttachmentDto
    {
        public Guid? EntityId { get; set; }

        [StringLength(100)]
        public string? EntityType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool? IsPrimary { get; set; }
    }

    public class BulkFileOperationItemDto
    {
        [Required]
        [StringLength(100)]
        public string EntityType { get; set; } = string.Empty;

        [Required]
        public Guid EntityId { get; set; }

        /// <summary>
        /// If provided with no File, the file will be deleted
        /// </summary>
        public Guid? FileId { get; set; }

        /// <summary>
        /// File to upload. If null and FileId is provided, the file will be deleted
        /// </summary>
        public IFormFile? File { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsPrimary { get; set; } = false;
    }

    public class BulkFileOperationResultDto
    {
        public Guid EntityId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public Guid? FileId { get; set; }
        public string Operation { get; set; } = string.Empty; // "uploaded", "deleted", "error"
        public string? FilePath { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
