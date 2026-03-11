using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class SalaryHistory : BaseEntity
    {
        [AuditDisplayName(typeof(Employee), nameof(Employee.FirstNameEn), nameof(Employee.FirstNameAr))]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public decimal? OldSalary { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string? Reason { get; set; }
    }
}
