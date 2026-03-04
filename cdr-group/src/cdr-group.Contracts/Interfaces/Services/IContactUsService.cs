using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.ContactUs;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IContactUsService : IBaseService<ContactUsDto, CreateContactUsDto, UpdateContactUsDto>
    {
        Task<PagedResult<ContactUsDto>> GetContactUsPagedAsync(ContactUsPagedRequest request);
    }
}
