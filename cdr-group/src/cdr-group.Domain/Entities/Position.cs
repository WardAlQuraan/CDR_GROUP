using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class Position : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public bool IsActive { get; set; } = true;

        // Department relationship (optional - position can be department-specific)
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Employees in this position
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
