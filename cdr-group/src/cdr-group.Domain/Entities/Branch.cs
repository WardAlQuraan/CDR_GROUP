using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; } = true;

        // Company relationship (required)
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
