using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class CompanyPreference : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string ValueEn { get; set; } = string.Empty;
        public string ValueAr { get; set; } = string.Empty;

        // Company relationship
        [AuditDisplayName(typeof(Company), nameof(Company.NameEn), nameof(Company.NameAr))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
