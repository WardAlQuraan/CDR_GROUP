using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.CompanyPartnershipFranchiseMechanism
{
    public class CompanyPartnershipFranchiseMechanismDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
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

    public class CreateCompanyPartnershipFranchiseMechanismDto
    {
        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class UpdateCompanyPartnershipFranchiseMechanismDto
    {
        [StringLength(2000)]
        public string? DescriptionEn { get; set; }

        [StringLength(2000)]
        public string? DescriptionAr { get; set; }

        public Guid? CompanyId { get; set; }
    }

    public class CompanyPartnershipFranchiseMechanismPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }
}
