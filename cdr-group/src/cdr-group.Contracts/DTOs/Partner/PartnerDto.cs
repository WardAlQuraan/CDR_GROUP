using System.ComponentModel.DataAnnotations;
using cdr_group.Contracts.Attributes;

namespace cdr_group.Contracts.DTOs.Partner
{
    public class PartnerDto
    {
        [ExcelIgnore]
        public Guid Id { get; set; }
        public string Status { get; set; } = string.Empty;
        [ExcelIgnore]
        public Guid CompanyId { get; set; }
        public string? CompanyNameEn { get; set; }
        public string? CompanyNameAr { get; set; }
        [ExcelIgnore]
        public Guid CityId { get; set; }
        public string? CityNameEn { get; set; }
        public string? CityNameAr { get; set; }
        public double? CityLatitude { get; set; }
        public double? CityLongitude { get; set; }
        public string? CountryNameEn { get; set; }
        public string? CountryNameAr { get; set; }
        [ExcelColumnName("CreatedDate")]
        public DateTime CreatedAt { get; set; }

    }

    public class CreatePartnerDto
    {
        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;
    }

    public class UpdatePartnerDto
    {
        public Guid? CompanyId { get; set; }

        public Guid? CityId { get; set; }

        public string? Status { get; set; }
    }

    public class PartnerPagedRequest : Common.PagedRequest
    {
        public Guid? CompanyId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? CountryId { get; set; }
        public string? Status { get; set; }
    }
}
