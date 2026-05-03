using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyClientReach;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers.CompanyControllers
{
    public class CompanyClientReachesController : BaseController<CompanyClientReachDto, CreateCompanyClientReachDto, UpdateCompanyClientReachDto, ICompanyClientReachService>
    {
        protected override string EntityName => "CompanyClientReach";

        public CompanyClientReachesController(ICompanyClientReachService companyClientReachService) : base(companyClientReachService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyClientReachDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyClientReachDto>>>> GetCompanyClientReachesPaged([FromQuery] CompanyClientReachPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyClientReachDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyClientReachDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyClientReachDto>>>> GetByCompany(Guid companyId)
        {
            var reaches = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyClientReachDto>>.SuccessResponse(reaches));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyClientReaches.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyClientReachDto>>> Create([FromBody] CreateCompanyClientReachDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyClientReaches.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyClientReachDto>>> Update(Guid id, [FromBody] UpdateCompanyClientReachDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyClientReaches.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        [HttpPost("{id:guid}/logo")]
        [HasPermission(Permissions.CompanyClientReaches.Update)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse<string>>> UploadLogo(Guid id, IFormFile file)
        {
            var url = await Service.UploadLogoAsync(id, file);
            return Ok(ApiResponse<string>.SuccessResponse(url, $"{EntityName} logo uploaded successfully."));
        }
    }
}
