using System.ComponentModel.DataAnnotations;

namespace cdr_group.Contracts.DTOs.City
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid CountryId { get; set; }
        public string? CountryNameEn { get; set; }
        public string? CountryNameAr { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCityDto
    {
        [Required]
        [StringLength(200)]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string NameAr { get; set; } = string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public Guid CountryId { get; set; }
    }

    public class UpdateCityDto
    {
        [StringLength(200)]
        public string? NameEn { get; set; }

        [StringLength(200)]
        public string? NameAr { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public Guid? CountryId { get; set; }
    }

    public class CityPagedRequest : Common.PagedRequest
    {
        public Guid? CountryId { get; set; }
    }
}
