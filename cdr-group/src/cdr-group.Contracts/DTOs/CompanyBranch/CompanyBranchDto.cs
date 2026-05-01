using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyBranch
{
    public class CompanyBranchDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? NickNameEn { get; set; }
        public string? NickNameAr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string? ImageUrl { get; set; }
        public string? LocationUrl { get; set; }
        public DateTime OpeningDate { get; set; }

        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;

        [ExcelIgnore]
        public Guid CityId { get; set; }
        public string CityNameEn { get; set; } = string.Empty;
        public string CityNameAr { get; set; } = string.Empty;

        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCompanyBranchDto
    {
        [Required]
        [StringLength(200)]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; } = string.Empty;

        [StringLength(200)]
        public string? NickNameEn { get; set; }

        [StringLength(200)]
        public string? NickNameAr { get; set; }

        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [StringLength(500)]
        public string? LocationUrl { get; set; }

        [Required]
        public DateTime OpeningDate { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public Guid CityId { get; set; }
    }

    public class UpdateCompanyBranchDto
    {
        [StringLength(200)]
        public string? NameEn { get; set; }

        [StringLength(200)]
        public string? NameAr { get; set; }

        [StringLength(200)]
        public string? NickNameEn { get; set; }

        [StringLength(200)]
        public string? NickNameAr { get; set; }

        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [StringLength(500)]
        public string? LocationUrl { get; set; }

        public DateTime? OpeningDate { get; set; }

        public Guid? CompanyId { get; set; }

        public Guid? CityId { get; set; }
    }

    public class CompanyBranchPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
        public Guid? CityId { get; set; }
    }
}
