using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities.Identity
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
