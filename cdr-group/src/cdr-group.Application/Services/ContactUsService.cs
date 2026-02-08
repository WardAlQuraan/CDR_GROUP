using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.ContactUs;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class ContactUsService : BaseService<ContactUs, ContactUsDto, CreateContactUsDto, UpdateContactUsDto>, IContactUsService
    {
        public ContactUsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<ContactUs> Repository => UnitOfWork.ContactUs;

        public override async Task<PagedResult<ContactUsDto>> GetPagedAsync(PagedRequest request)
        {
            var (items, totalCount) = await UnitOfWork.ContactUs.GetContactUsPagedAsync(request);
            var dtos = Mapper.Map<List<ContactUsDto>>(items);
            return new PagedResult<ContactUsDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
