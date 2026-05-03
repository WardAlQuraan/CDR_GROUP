using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class CompanyBranch : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? NickNameEn { get; set; }
        public string? NickNameAr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string? ImageUrl { get; set; }
        public string? LocationUrl { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime OpeningDate { get; set; }

        // Company relationship
        [AuditDisplayName(typeof(Company), nameof(Company.NameEn), nameof(Company.NameAr))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        // City relationship
        [AuditDisplayName(typeof(City), nameof(City.NameEn), nameof(City.NameAr))]
        public Guid CityId { get; set; }
        public City City { get; set; } = null!;
    }
}
