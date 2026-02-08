using cdr_group.Domain.Entities.Base;

namespace cdr_group.Domain.Entities
{
    public class ContactUs : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
