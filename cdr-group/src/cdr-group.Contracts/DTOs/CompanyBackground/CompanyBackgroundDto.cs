using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyBackground
{
    public class CompanyBackgroundDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;

        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCompanyBackgroundDto
    {
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateCompanyBackgroundDto
    {
        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public Guid? CompanyId { get; set; }
    }

    public class CompanyBackgroundPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }
}
