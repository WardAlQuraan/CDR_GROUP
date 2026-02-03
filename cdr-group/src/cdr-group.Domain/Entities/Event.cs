using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string? EventUrl { get; set; }
        public DateTime? EventDate { get; set; }

        // Optional relationship to Department
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
