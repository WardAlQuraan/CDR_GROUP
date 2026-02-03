namespace cdr_group.Contracts.DTOs.Identity
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsSystemRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Permissions { get; set; } = new();
    }

    public class CreateRoleDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<Guid>? PermissionIds { get; set; }
    }

    public class UpdateRoleDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class AssignPermissionsDto
    {
        public List<Guid> PermissionIds { get; set; } = new();
    }
}
