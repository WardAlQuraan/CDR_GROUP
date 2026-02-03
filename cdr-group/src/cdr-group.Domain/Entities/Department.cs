using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public bool IsActive { get; set; } = true;

        // Self-referencing relationship for parent department
        public Guid? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }
        public virtual ICollection<Department> SubDepartments { get; set; } = new List<Department>();

        // Manager of the department
        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        // Employees in this department
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
