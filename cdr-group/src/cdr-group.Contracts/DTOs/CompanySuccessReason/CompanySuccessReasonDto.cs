using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanySuccessReason
{
    public class CompanySuccessReasonDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string ReasonEn { get; set; } = string.Empty;
        public string ReasonAr { get; set; } = string.Empty;

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;

        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCompanySuccessReasonDto
    {
        [Required]
        [StringLength(1000)]
        public string ReasonEn { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string ReasonAr { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateCompanySuccessReasonDto
    {
        [StringLength(1000)]
        public string? ReasonEn { get; set; }

        [StringLength(1000)]
        public string? ReasonAr { get; set; }

        public Guid? CompanyId { get; set; }
    }

    public class CompanySuccessReasonPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }
}
