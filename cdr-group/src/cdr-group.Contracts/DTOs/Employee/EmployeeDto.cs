using System.ComponentModel.DataAnnotations;

namespace cdr_group.Contracts.DTOs.Employee
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstNameEn { get; set; } = string.Empty;
        public string LastNameEn { get; set; } = string.Empty;
        public string FirstNameAr { get; set; } = string.Empty;
        public string LastNameAr { get; set; } = string.Empty;
        public string FullNameEn => $"{FirstNameEn} {LastNameEn}";
        public string FullNameAr => $"{FirstNameAr} {LastNameAr}";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; }

        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public Guid? PositionId { get; set; }
        public string? PositionName { get; set; }

        public Guid? ManagerId { get; set; }
        public EmployeeBasicDto? Manager { get; set; }

        public Guid? UserId { get; set; }
        public string? Username { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // File attachment path (e.g., profile photo)
        public string? FilePath { get; set; }
    }

    public class EmployeeBasicDto
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstNameEn { get; set; } = string.Empty;
        public string LastNameEn { get; set; } = string.Empty;
        public string FirstNameAr { get; set; } = string.Empty;
        public string LastNameAr { get; set; } = string.Empty;
        public string FullNameEn => $"{FirstNameEn} {LastNameEn}";
        public string FullNameAr => $"{FirstNameAr} {LastNameAr}";
        public Guid? PositionId { get; set; }
        public string? PositionName { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }

    public class EmployeeWithSubordinatesDto : EmployeeDto
    {
        public List<EmployeeBasicDto> Subordinates { get; set; } = new();
    }

    public class EmployeeTreeNodeDto
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstNameEn { get; set; } = string.Empty;
        public string LastNameEn { get; set; } = string.Empty;
        public string FirstNameAr { get; set; } = string.Empty;
        public string LastNameAr { get; set; } = string.Empty;
        public string FullNameEn => $"{FirstNameEn} {LastNameEn}";
        public string FullNameAr => $"{FirstNameAr} {LastNameAr}";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        public Guid? PositionId { get; set; }
        public string? PositionNameEn { get; set; }
        public string? PositionNameAr { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentNameEn { get; set; } = string.Empty;
        public string DepartmentNameAr { get; set; } = string.Empty;
        public Guid? CompanyId { get; set; }
        public string? CompanyNameEn { get; set; }
        public string? CompanyNameAr { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? FilePath { get; set; }
        public List<EmployeeTreeNodeDto> Children { get; set; } = new();
    }

    public class GetTreeRequest
    {
        public Guid? CompanyId { get; set; }
        public string? CompanyCode { get; set; }
        public Guid? DepartmentId { get; set; }
    }

    public class CreateEmployeeDto
    {
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstNameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastNameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstNameAr { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastNameAr { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public DateTime? HireDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Salary { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public Guid DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? UserId { get; set; }
    }

    public class UpdateEmployeeDto
    {
        [StringLength(50)]
        public string? EmployeeCode { get; set; }

        [StringLength(100)]
        public string? FirstNameEn { get; set; }

        [StringLength(100)]
        public string? LastNameEn { get; set; }

        [StringLength(100)]
        public string? FirstNameAr { get; set; }

        [StringLength(100)]
        public string? LastNameAr { get; set; }

        [EmailAddress]
        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public DateTime? HireDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Salary { get; set; }

        [StringLength(500)]
        public string? SalaryChangeReason { get; set; }

        public bool? IsActive { get; set; }

        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? UserId { get; set; }
    }
}
