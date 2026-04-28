using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyForm;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompanyFormsController : BaseController<CompanyFormDto, CreateCompanyFormDto, UpdateCompanyFormDto, ICompanyFormService>
    {
        protected override string EntityName => "CompanyForm";

        public CompanyFormsController(ICompanyFormService companyFormService) : base(companyFormService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyFormDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyFormDto>>>> GetCompanyFormsPaged([FromQuery] CompanyFormPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyFormDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyFormDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyFormDto>>>> GetByCompany(Guid companyId)
        {
            var forms = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyFormDto>>.SuccessResponse(forms));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyForms.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyFormDto>>> Create([FromBody] CreateCompanyFormDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyForms.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyFormDto>>> Update(Guid id, [FromBody] UpdateCompanyFormDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyForms.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
