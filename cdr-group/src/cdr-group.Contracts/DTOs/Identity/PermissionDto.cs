namespace cdr_group.Contracts.DTOs.Identity
{
    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Module { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreatePermissionDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Module { get; set; }
    }

    public class UpdatePermissionDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Module { get; set; }
    }
}
