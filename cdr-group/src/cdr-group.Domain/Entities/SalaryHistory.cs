using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class SalaryHistory : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public decimal? OldSalary { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string? Reason { get; set; }
    }
}
