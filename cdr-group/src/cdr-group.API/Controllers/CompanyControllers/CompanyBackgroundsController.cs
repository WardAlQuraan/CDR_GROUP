using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyBackground;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers.CompanyControllers
{
    public class CompanyBackgroundsController : BaseController<CompanyBackgroundDto, CreateCompanyBackgroundDto, UpdateCompanyBackgroundDto, ICompanyBackgroundService>
    {
        protected override string EntityName => "CompanyBackground";

        public CompanyBackgroundsController(ICompanyBackgroundService companyBackgroundService) : base(companyBackgroundService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyBackgroundDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyBackgroundDto>>>> GetCompanyBackgroundsPaged([FromQuery] CompanyBackgroundPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyBackgroundDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyBackgroundDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyBackgroundDto>>>> GetByCompany(Guid companyId)
        {
            var backgrounds = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyBackgroundDto>>.SuccessResponse(backgrounds));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyBackgrounds.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyBackgroundDto>>> Create([FromBody] CreateCompanyBackgroundDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyBackgrounds.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyBackgroundDto>>> Update(Guid id, [FromBody] UpdateCompanyBackgroundDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyBackgrounds.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
