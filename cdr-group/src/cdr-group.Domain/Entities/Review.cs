using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int NumberOfStars { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsVisible { get; set; }

        [AuditDisplayName(typeof(Company), nameof(Company.NameEn), nameof(Company.NameAr))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
