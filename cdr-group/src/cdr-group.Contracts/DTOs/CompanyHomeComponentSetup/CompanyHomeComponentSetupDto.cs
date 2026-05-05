using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyHomeComponentSetup
{
    public class CompanyHomeComponentSetupDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string ComponentCode { get; set; } = string.Empty;
        public string? CompanyTitleDescriptionCode { get; set; }
        public string? PreferenceTitleCode { get; set; }
        public string? PreferenceDescriptionCode { get; set; }
        public int Rank { get; set; }

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;

        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCompanyHomeComponentSetupDto
    {
        [Required]
        [StringLength(100)]
        public string ComponentCode { get; set; } = string.Empty;

        [StringLength(100)]
        public string? CompanyTitleDescriptionCode { get; set; }

        [StringLength(100)]
        public string? PreferenceTitleCode { get; set; }

        [StringLength(100)]
        public string? PreferenceDescriptionCode { get; set; }

        public int Rank { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateCompanyHomeComponentSetupDto
    {
        [StringLength(100)]
        public string? ComponentCode { get; set; }

        [StringLength(100)]
        public string? CompanyTitleDescriptionCode { get; set; }

        [StringLength(100)]
        public string? PreferenceTitleCode { get; set; }

        [StringLength(100)]
        public string? PreferenceDescriptionCode { get; set; }

        public int? Rank { get; set; }

        public Guid? CompanyId { get; set; }
    }

    public class CompanyHomeComponentSetupPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
        public string? ComponentCode { get; set; }
    }

    public class ReorderCompanyHomeComponentSetupItemDto
    {
        [Required]
        public Guid Id { get; set; }
        public int Rank { get; set; }
    }
}
