using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class CompanyTitleDescription : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string? TitleEn { get; set; }
        public string? TitleAr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }

        // Company relationship
        [AuditDisplayName(typeof(Company), nameof(Company.NameEn), nameof(Company.NameAr))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
