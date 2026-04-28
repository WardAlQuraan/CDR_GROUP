using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class Company : BaseEntity
    {
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
        public string? TitleEn { get; set; }
        public string? TitleAr { get; set; }
        public string? PrimaryColor { get; set; }
        public string? SecondaryColor { get; set; }
        public string? OpeningStartDay { get; set; }
        public string? OpeningEndDay { get; set; }
        public TimeSpan? OpeningStartTime { get; set; }
        public TimeSpan? OpeningEndTime { get; set; }
        public string? OpeningHoursNoteEn { get; set; }
        public string? OpeningHoursNoteAr { get; set; }
        public string? PartnershipFormUrl { get; set; }
        public string? Logo { get; set; }
        public int NumberOfEmployees { get; set; }
        public bool IsActive { get; set; } = true;

        // Self-referencing parent
        [AuditDisplayName(typeof(Company), nameof(NameEn), nameof(NameAr))]
        public Guid? ParentId { get; set; }
        public Company? Parent { get; set; }
        public virtual ICollection<Company> Children { get; set; } = new List<Company>();

        // Employees in this company
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        // Events in this company
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

        // Contacts for this company
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();

        // Partners for this company
        public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();

        // Background images for this company
        public virtual ICollection<CompanyBackground> CompanyBackgrounds { get; set; } = new List<CompanyBackground>();

        // Forms for this company
        public virtual ICollection<CompanyForm> CompanyForms { get; set; } = new List<CompanyForm>();
    }
}
