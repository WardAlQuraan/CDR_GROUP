using cdr_group.Contracts.DTOs.ContactUs;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IContactUsService : IBaseService<ContactUsDto, CreateContactUsDto, UpdateContactUsDto>
    {
    }
}
