using System.ComponentModel.DataAnnotations;

namespace cdr_group.Contracts.DTOs.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public bool IsActive { get; set; }

        public Guid? ParentDepartmentId { get; set; }
        public DepartmentBasicDto? ParentDepartment { get; set; }

        public Guid? ManagerId { get; set; }
        public string? ManagerName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class DepartmentBasicDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
    }

    public class DepartmentWithSubDepartmentsDto : DepartmentDto
    {
        public List<DepartmentBasicDto> SubDepartments { get; set; } = new();
    }

    public class DepartmentWithEmployeesDto : DepartmentDto
    {
        public int EmployeeCount { get; set; }
    }

    public class CreateDepartmentDto
    {
        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; } = string.Empty;

        [StringLength(500)]
        public string? DescriptionEn { get; set; }

        [StringLength(500)]
        public string? DescriptionAr { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid? ParentDepartmentId { get; set; }
        public Guid? ManagerId { get; set; }
    }

    public class UpdateDepartmentDto
    {
        [StringLength(50)]
        public string? Code { get; set; }

        [StringLength(200)]
        public string? NameEn { get; set; }

        [StringLength(200)]
        public string? NameAr { get; set; }

        [StringLength(500)]
        public string? DescriptionEn { get; set; }

        [StringLength(500)]
        public string? DescriptionAr { get; set; }

        public bool? IsActive { get; set; }

        public Guid? ParentDepartmentId { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
