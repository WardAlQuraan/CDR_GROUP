using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyTitleDescription;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers.CompanyControllers
{
    public class CompanyTitleDescriptionsController : BaseController<CompanyTitleDescriptionDto, CreateCompanyTitleDescriptionDto, UpdateCompanyTitleDescriptionDto, ICompanyTitleDescriptionService>
    {
        protected override string EntityName => "CompanyTitleDescription";

        public CompanyTitleDescriptionsController(ICompanyTitleDescriptionService companyTitleDescriptionService) : base(companyTitleDescriptionService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyTitleDescriptionDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyTitleDescriptionDto>>>> GetCompanyTitleDescriptionsPaged([FromQuery] CompanyTitleDescriptionPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyTitleDescriptionDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyTitleDescriptionDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyTitleDescriptionDto>>>> GetByCompany(Guid companyId)
        {
            var items = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyTitleDescriptionDto>>.SuccessResponse(items));
        }

        [HttpGet("by-company/{companyId:guid}/code/{code}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyTitleDescriptionDto>>>> GetByCompanyAndCode(Guid companyId, string code)
        {
            var items = await Service.GetByCompanyAndCodeAsync(companyId, code);
            return Ok(ApiResponse<IEnumerable<CompanyTitleDescriptionDto>>.SuccessResponse(items));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyTitleDescriptions.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyTitleDescriptionDto>>> Create([FromBody] CreateCompanyTitleDescriptionDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyTitleDescriptions.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyTitleDescriptionDto>>> Update(Guid id, [FromBody] UpdateCompanyTitleDescriptionDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyTitleDescriptions.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
