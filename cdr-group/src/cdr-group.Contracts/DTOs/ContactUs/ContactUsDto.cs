using System.ComponentModel.DataAnnotations;

namespace cdr_group.Contracts.DTOs.ContactUs
{
    public class ContactUsDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
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
    }
}
