using System.ComponentModel.DataAnnotations;

namespace cdr_group.Contracts.DTOs.Company
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CompanyBasicDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
    }

    public class CreateCompanyDto
    {
        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; } = string.Empty;

        [StringLength(500)]
        public string? DescriptionEn { get; set; }

        [StringLength(500)]
        public string? DescriptionAr { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateCompanyDto
    {
        [StringLength(50)]
        public string? Code { get; set; }

        [StringLength(200)]
        public string? NameEn { get; set; }

        [StringLength(200)]
        public string? NameAr { get; set; }

        [StringLength(500)]
        public string? DescriptionEn { get; set; }

        [StringLength(500)]
        public string? DescriptionAr { get; set; }

        public bool? IsActive { get; set; }
    }
}
