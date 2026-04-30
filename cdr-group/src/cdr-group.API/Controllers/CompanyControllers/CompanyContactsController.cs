using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyContact;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers.CompanyControllers
{
    public class CompanyContactsController : BaseController<CompanyContactDto, CreateCompanyContactDto, UpdateCompanyContactDto, ICompanyContactService>
    {
        protected override string EntityName => "CompanyContact";

        public CompanyContactsController(ICompanyContactService companyContactService) : base(companyContactService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyContactDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyContactDto>>>> GetCompaniesPaged([FromQuery] CompanyContactPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyContactDto>>.SuccessResponse(result));
        }
                                        
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyContactDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyContactDto>>>> GetByCompany(Guid companyId)
        {
            var contacts = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyContactDto>>.SuccessResponse(contacts));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyContacts.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyContactDto>>> Create([FromBody] CreateCompanyContactDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyContacts.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyContactDto>>> Update(Guid id, [FromBody] UpdateCompanyContactDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyContacts.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
