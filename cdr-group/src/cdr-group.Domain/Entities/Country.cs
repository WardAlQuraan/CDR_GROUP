using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual ICollection<City> Cities { get; set; } = new List<City>();
    }
}
