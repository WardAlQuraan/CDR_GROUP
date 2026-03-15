using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.ContactUs
{
    public class ContactUsDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string? CompanyNameEn { get; set; }
        public string? CompanyNameAr { get; set; }
        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }

    }

    public class CreateContactUsDto
    {
        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Message { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class ContactUsPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
    }

    public class UpdateContactUsDto
    {
        [StringLength(200)]
        public string? FullName { get; set; }

        [EmailAddress]
        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(2000)]
        public string? Message { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
