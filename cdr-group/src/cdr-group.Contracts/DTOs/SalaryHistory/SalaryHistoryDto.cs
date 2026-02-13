using System.ComponentModel.DataAnnotations;

namespace cdr_group.Contracts.DTOs.SalaryHistory
{
    public class SalaryHistoryDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeNameEn { get; set; } = string.Empty;
        public string EmployeeNameAr { get; set; } = string.Empty;
        public decimal? OldSalary { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateSalaryHistoryDto
    {
        [Required]
        public Guid EmployeeId { get; set; }

        public decimal? OldSalary { get; set; }

        [Required]
        public decimal NewSalary { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        [StringLength(500)]
        public string? Reason { get; set; }
    }

    public class UpdateSalaryHistoryDto
    {
        public decimal? OldSalary { get; set; }

        public decimal? NewSalary { get; set; }

        public DateTime? EffectiveDate { get; set; }

        [StringLength(500)]
        public string? Reason { get; set; }
    }
}
