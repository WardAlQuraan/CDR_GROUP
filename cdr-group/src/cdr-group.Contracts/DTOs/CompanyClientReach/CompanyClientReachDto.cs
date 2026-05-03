using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyClientReach
{
    public class CompanyClientReachDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string ClientNameEn { get; set; } = string.Empty;
        public string ClientNameAr { get; set; } = string.Empty;
        public string? ClientLogoUrl { get; set; }
        public string Reach { get; set; } = string.Empty;
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

    public class CreateCompanyClientReachDto
    {
        [Required]
        [StringLength(200)]
        public string ClientNameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string ClientNameAr { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Reach { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateCompanyClientReachDto
    {
        [StringLength(200)]
        public string? ClientNameEn { get; set; }

        [StringLength(200)]
        public string? ClientNameAr { get; set; }

        [StringLength(200)]
        public string? Reach { get; set; }

        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        public Guid? CompanyId { get; set; }
    }

    public class CompanyClientReachPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }
}
