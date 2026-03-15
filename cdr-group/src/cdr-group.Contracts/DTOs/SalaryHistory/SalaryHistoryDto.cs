using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.SalaryHistory
{
    public class SalaryHistoryDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        [ExcelIgnore]
        public Guid EmployeeId { get; set; }
        public string EmployeeNameEn { get; set; } = string.Empty;
        public string EmployeeNameAr { get; set; } = string.Empty;
        public decimal? OldSalary { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string? Reason { get; set; }
        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }
        [ExcelColumnName("ModifiedDate")]
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
