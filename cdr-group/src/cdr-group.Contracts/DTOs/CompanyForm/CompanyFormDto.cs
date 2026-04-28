using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyForm
{
    public class CompanyFormDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string FormUrl { get; set; } = string.Empty;
        public string FormNameEn { get; set; } = string.Empty;
        public string FormNameAr { get; set; } = string.Empty;

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;

        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCompanyFormDto
    {
        [Required]
        [StringLength(500)]
        public string FormUrl { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string FormNameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string FormNameAr { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateCompanyFormDto
    {
        [StringLength(500)]
        public string? FormUrl { get; set; }

        [StringLength(200)]
        public string? FormNameEn { get; set; }

        [StringLength(200)]
        public string? FormNameAr { get; set; }

        public Guid? CompanyId { get; set; }
    }

    public class CompanyFormPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }
}
