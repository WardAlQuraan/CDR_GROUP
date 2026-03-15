using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.Review
{
    public class ReviewDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public int NumberOfStars { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsVisible { get; set; }
        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string? CompanyNameEn { get; set; }
        public string? CompanyNameAr { get; set; }
        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
    }

    public class CreateReviewDto
    {
        [Required]
        [Range(1, 5)]
        public int NumberOfStars { get; set; }

        [Required]
        [StringLength(2000)]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class ReviewPagedRequest : Common.PagedRequest
    {
        public bool? IsVisible { get; set; }
        public Guid? CompanyId { get; set; }
    }

    public class UpdateReviewDto
    {
        [Range(1, 5)]
        public int? NumberOfStars { get; set; }

        [StringLength(2000)]
        public string? Comment { get; set; }

        public bool? IsVisible { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
