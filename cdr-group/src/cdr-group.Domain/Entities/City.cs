using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class City : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
