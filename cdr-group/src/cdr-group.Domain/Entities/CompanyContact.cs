using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class CompanyContact : BaseEntity
    {
        public string Icon { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        // Company relationship
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
