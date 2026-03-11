using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;
using cdr_group.Domain.Enums;

namespace cdr_group.Domain.Entities
{
    public class Partner : BaseEntity
    {
        public PartnerStatus Status { get; set; }

        [AuditDisplayName(typeof(Company), nameof(Company.NameEn), nameof(Company.NameAr))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }

        [AuditDisplayName(typeof(City), nameof(City.NameEn), nameof(City.NameAr))]
        public Guid CityId { get; set; }
        public City? City { get; set; }
    }
}
