using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyContact
{
    public class CompanyContactDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;
        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCompanyContactDto
    {
        [Required]
        [StringLength(100)]
        public string Icon { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Value { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class CompanyContactPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }

    public class UpdateCompanyContactDto
    {
        [StringLength(100)]
        public string? Icon { get; set; }

        [StringLength(200)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Value { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
