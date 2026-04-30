using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyDistinguish
{
    public class CompanyDistinguishDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;

        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCompanyDistinguishDto
    {
        [Required]
        [StringLength(500)]
        public string TitleEn { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string TitleAr { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateCompanyDistinguishDto
    {
        [StringLength(500)]
        public string? TitleEn { get; set; }

        [StringLength(500)]
        public string? TitleAr { get; set; }

        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        public Guid? CompanyId { get; set; }
    }

    public class CompanyDistinguishPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }
}
