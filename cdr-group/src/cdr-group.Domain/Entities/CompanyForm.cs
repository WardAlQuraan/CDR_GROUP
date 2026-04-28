using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class CompanyForm : BaseEntity
    {
        public string FormUrl { get; set; } = string.Empty;
        public string FormNameEn { get; set; } = string.Empty;
        public string FormNameAr { get; set; } = string.Empty;

        // Company relationship
        [AuditDisplayName(typeof(Company), nameof(Company.NameEn), nameof(Company.NameAr))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
