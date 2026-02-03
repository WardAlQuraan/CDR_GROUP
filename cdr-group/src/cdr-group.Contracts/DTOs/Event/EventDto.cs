using System.ComponentModel.DataAnnotations;

namespace cdr_group.Contracts.DTOs.Event
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string? EventUrl { get; set; }
        public DateTime? EventDate { get; set; }

        public Guid? DepartmentId { get; set; }
        public string? DepartmentNameEn { get; set; }
        public string? DepartmentNameAr { get; set; }

        public string? PrimaryFileUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateEventDto
    {
        [Required]
        [StringLength(200)]
        public string TitleEn { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string TitleAr { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        [StringLength(500)]
        [Url]
        public string? EventUrl { get; set; }

        public DateTime? EventDate { get; set; }

        public Guid? DepartmentId { get; set; }
    }

    public class UpdateEventDto
    {
        [StringLength(200)]
        public string? TitleEn { get; set; }

        [StringLength(200)]
        public string? TitleAr { get; set; }

        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        [StringLength(500)]
        [Url]
        public string? EventUrl { get; set; }

        public DateTime? EventDate { get; set; }

        public Guid? DepartmentId { get; set; }
    }
}
