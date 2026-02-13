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
        public bool IsActive { get; set; } = true;

        // Employees in this company
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        // Events in this company
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
