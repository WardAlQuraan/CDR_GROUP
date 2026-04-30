using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyFinancialClausesRights;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompanyFinancialClausesRightsController : BaseController<CompanyFinancialClausesRightsDto, CreateCompanyFinancialClausesRightsDto, UpdateCompanyFinancialClausesRightsDto, ICompanyFinancialClausesRightsService>
    {
        protected override string EntityName => "CompanyFinancialClausesRights";

        public CompanyFinancialClausesRightsController(ICompanyFinancialClausesRightsService companyFinancialClausesRightsService) : base(companyFinancialClausesRightsService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyFinancialClausesRightsDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyFinancialClausesRightsDto>>>> GetCompanyFinancialClausesRightsPaged([FromQuery] CompanyFinancialClausesRightsPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyFinancialClausesRightsDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyFinancialClausesRightsDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyFinancialClausesRightsDto>>>> GetByCompany(Guid companyId)
        {
            var clauses = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyFinancialClausesRightsDto>>.SuccessResponse(clauses));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyFinancialClausesRights.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyFinancialClausesRightsDto>>> Create([FromBody] CreateCompanyFinancialClausesRightsDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyFinancialClausesRights.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyFinancialClausesRightsDto>>> Update(Guid id, [FromBody] UpdateCompanyFinancialClausesRightsDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyFinancialClausesRights.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
