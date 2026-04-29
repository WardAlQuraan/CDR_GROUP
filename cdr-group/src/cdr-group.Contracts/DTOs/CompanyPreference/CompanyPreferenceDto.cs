using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyPreference
{
    public class CompanyPreferenceDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string ValueEn { get; set; } = string.Empty;
        public string ValueAr { get; set; } = string.Empty;

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;

        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCompanyPreferenceDto
    {
        [Required]
        [StringLength(100)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string ValueEn { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string ValueAr { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateCompanyPreferenceDto
    {
        [StringLength(100)]
        public string? Code { get; set; }

        [StringLength(1000)]
        public string? ValueEn { get; set; }

        [StringLength(1000)]
        public string? ValueAr { get; set; }

        public Guid? CompanyId { get; set; }
    }

    public class CompanyPreferencePagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
        public string? Code { get; set; }
    }
}
