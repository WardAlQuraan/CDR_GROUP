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
        public string? StoryEn { get; set; }
        public string? StoryAr { get; set; }
        public string? MissionEn { get; set; }
        public string? MissionAr { get; set; }
        public string? VisionEn { get; set; }
        public string? VisionAr { get; set; }
        public string? PrimaryColor { get; set; }
        public string? SecondaryColor { get; set; }
        public string? TitleEn { get; set; }
        public string? TitleAr { get; set; }
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

        [StringLength(2000)]
        public string? StoryEn { get; set; }

        [StringLength(2000)]
        public string? StoryAr { get; set; }

        [StringLength(1000)]
        public string? MissionEn { get; set; }

        [StringLength(1000)]
        public string? MissionAr { get; set; }

        [StringLength(1000)]
        public string? VisionEn { get; set; }

        [StringLength(1000)]
        public string? VisionAr { get; set; }

        [StringLength(20)]
        public string? PrimaryColor { get; set; }

        [StringLength(20)]
        public string? SecondaryColor { get; set; }

        [StringLength(500)]
        public string? TitleEn { get; set; }

        [StringLength(500)]
        public string? TitleAr { get; set; }

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

        [StringLength(2000)]
        public string? StoryEn { get; set; }

        [StringLength(2000)]
        public string? StoryAr { get; set; }

        [StringLength(1000)]
        public string? MissionEn { get; set; }

        [StringLength(1000)]
        public string? MissionAr { get; set; }

        [StringLength(1000)]
        public string? VisionEn { get; set; }

        [StringLength(1000)]
        public string? VisionAr { get; set; }

        [StringLength(20)]
        public string? PrimaryColor { get; set; }

        [StringLength(20)]
        public string? SecondaryColor { get; set; }

        [StringLength(500)]
        public string? TitleEn { get; set; }

        [StringLength(500)]
        public string? TitleAr { get; set; }

        public bool? IsActive { get; set; }
    }
}
