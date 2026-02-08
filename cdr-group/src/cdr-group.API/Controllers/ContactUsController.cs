using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.ContactUs;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class ContactUsController : BaseController<ContactUsDto, CreateContactUsDto, UpdateContactUsDto, IContactUsService>
    {
        protected override string EntityName => "ContactUs";

        public ContactUsController(IContactUsService contactUsService) : base(contactUsService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.ContactUs.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<ContactUsDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.ContactUs.Read)]
        public override async Task<ActionResult<ApiResponse<ContactUsDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<ContactUsDto>>> Create([FromBody] CreateContactUsDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.ContactUs.Update)]
        public override async Task<ActionResult<ApiResponse<ContactUsDto>>> Update(Guid id, [FromBody] UpdateContactUsDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.ContactUs.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
