using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyGeographicExpansion;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompanyGeographicExpansionsController : BaseController<CompanyGeographicExpansionDto, CreateCompanyGeographicExpansionDto, UpdateCompanyGeographicExpansionDto, ICompanyGeographicExpansionService>
    {
        protected override string EntityName => "CompanyGeographicExpansion";

        public CompanyGeographicExpansionsController(ICompanyGeographicExpansionService companyGeographicExpansionService) : base(companyGeographicExpansionService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyGeographicExpansionDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyGeographicExpansionDto>>>> GetCompanyGeographicExpansionsPaged([FromQuery] CompanyGeographicExpansionPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyGeographicExpansionDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyGeographicExpansionDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyGeographicExpansionDto>>>> GetByCompany(Guid companyId)
        {
            var expansions = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyGeographicExpansionDto>>.SuccessResponse(expansions));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyGeographicExpansions.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyGeographicExpansionDto>>> Create([FromBody] CreateCompanyGeographicExpansionDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyGeographicExpansions.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyGeographicExpansionDto>>> Update(Guid id, [FromBody] UpdateCompanyGeographicExpansionDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyGeographicExpansions.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
