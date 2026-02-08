using cdr_group.Domain.Entities.Base;
using cdr_group.Domain.Entities.Identity;

namespace cdr_group.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstNameEn { get; set; } = string.Empty;
        public string LastNameEn { get; set; } = string.Empty;
        public string FirstNameAr { get; set; } = string.Empty;
        public string LastNameAr { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? HireDate { get; set; }

        // Position relationship
        public Guid? PositionId { get; set; }
        public Position? Position { get; set; }
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; } = true;

        // Department relationship
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        // Self-referencing relationship for manager
        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public virtual ICollection<Employee> Subordinates { get; set; } = new List<Employee>();

        // Optional relationship to User (nullable)
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
