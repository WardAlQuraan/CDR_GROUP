using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class CompanyHomeComponentSetup : BaseEntity
    {
        public string ComponentCode { get; set; } = string.Empty;
        public string? CompanyTitleDescriptionCode { get; set; }
        public string? PreferenceTitleCode { get; set; }
        public string? PreferenceDescriptionCode { get; set; }
        public int Rank { get; set; }

        // Company relationship
        [AuditDisplayName(typeof(Company), nameof(Company.NameEn), nameof(Company.NameAr))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
