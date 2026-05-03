using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class CompanyClientReach : BaseEntity
    {
        public string ClientNameEn { get; set; } = string.Empty;
        public string ClientNameAr { get; set; } = string.Empty;
        public string? ClientLogoUrl { get; set; }
        public string Reach { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }

        // Company relationship
        [AuditDisplayName(typeof(Company), nameof(Company.NameEn), nameof(Company.NameAr))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
