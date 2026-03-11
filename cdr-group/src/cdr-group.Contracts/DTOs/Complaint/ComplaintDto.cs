using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.Complaint
{
    public class ComplaintDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string? CompanyNameEn { get; set; }
        public string? CompanyNameAr { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateComplaintDto
    {
        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Message { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class ComplaintPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }

    public class UpdateComplaintDto
    {
        [StringLength(200)]
        public string? FullName { get; set; }

        [EmailAddress]
        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? Subject { get; set; }

        [StringLength(2000)]
        public string? Message { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
