using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string? StoryEn { get; set; }
        public string? StoryAr { get; set; }
        public string? MissionEn { get; set; }
        public string? MissionAr { get; set; }
        public string? VisionEn { get; set; }
        public string? VisionAr { get; set; }
        public string? PrimaryColor { get; set; }
        public string? SecondaryColor { get; set; }
        public string? TitleEn { get; set; }
        public string? TitleAr { get; set; }
        public bool IsActive { get; set; } = true;

        // Employees in this company
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        // Branches of this company
        public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

        // Events in this company
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
