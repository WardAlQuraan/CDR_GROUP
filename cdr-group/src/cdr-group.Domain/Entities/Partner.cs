using cdr_group.Domain.Entities.Base;
using cdr_group.Domain.Enums;

namespace cdr_group.Domain.Entities
{
    public class Partner : BaseEntity
    {
        public PartnerStatus Status { get; set; }

        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }

        public Guid CityId { get; set; }
        public City? City { get; set; }
    }
}
