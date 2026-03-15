using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.Event
{
    public class EventDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string? EventUrl { get; set; }
        public DateTime? EventDate { get; set; }

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;

        public string? PrimaryFileUrl { get; set; }

        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
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

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class EventPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
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

        public Guid? CompanyId { get; set; }
    }
}
