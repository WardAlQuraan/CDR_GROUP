using System.ComponentModel.DataAnnotations;

namespace cdr_group.Contracts.DTOs.Branch
{
    public class BranchDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyNameEn { get; set; } = string.Empty;
        public string CompanyNameAr { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateBranchDto
    {
        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Address { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateBranchDto
    {
        [StringLength(50)]
        public string? Code { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsActive { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
