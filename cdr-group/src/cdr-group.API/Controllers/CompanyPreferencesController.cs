using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyPreference;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompanyPreferencesController : BaseController<CompanyPreferenceDto, CreateCompanyPreferenceDto, UpdateCompanyPreferenceDto, ICompanyPreferenceService>
    {
        protected override string EntityName => "CompanyPreference";

        public CompanyPreferencesController(ICompanyPreferenceService companyPreferenceService) : base(companyPreferenceService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyPreferenceDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyPreferenceDto>>>> GetCompanyPreferencesPaged([FromQuery] CompanyPreferencePagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyPreferenceDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyPreferenceDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyPreferenceDto>>>> GetByCompany(Guid companyId)
        {
            var preferences = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyPreferenceDto>>.SuccessResponse(preferences));
        }

        [HttpGet("by-company/{companyId:guid}/code/{code}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<CompanyPreferenceDto>>> GetByCompanyAndCode(Guid companyId, string code)
        {
            var preference = await Service.GetByCompanyAndCodeAsync(companyId, code);
            
            return Ok(ApiResponse<CompanyPreferenceDto>.SuccessResponse(preference));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyPreferences.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyPreferenceDto>>> Create([FromBody] CreateCompanyPreferenceDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyPreferences.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyPreferenceDto>>> Update(Guid id, [FromBody] UpdateCompanyPreferenceDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyPreferences.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
