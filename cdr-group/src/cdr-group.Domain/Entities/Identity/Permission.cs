using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities.Identity
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Module { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
