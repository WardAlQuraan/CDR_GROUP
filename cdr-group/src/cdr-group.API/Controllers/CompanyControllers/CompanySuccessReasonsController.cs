using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanySuccessReason;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers.CompanyControllers
{
    public class CompanySuccessReasonsController : BaseController<CompanySuccessReasonDto, CreateCompanySuccessReasonDto, UpdateCompanySuccessReasonDto, ICompanySuccessReasonService>
    {
        protected override string EntityName => "CompanySuccessReason";

        public CompanySuccessReasonsController(ICompanySuccessReasonService companySuccessReasonService) : base(companySuccessReasonService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanySuccessReasonDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanySuccessReasonDto>>>> GetCompanySuccessReasonsPaged([FromQuery] CompanySuccessReasonPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanySuccessReasonDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanySuccessReasonDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanySuccessReasonDto>>>> GetByCompany(Guid companyId)
        {
            var reasons = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanySuccessReasonDto>>.SuccessResponse(reasons));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanySuccessReasons.Create)]
        public override async Task<ActionResult<ApiResponse<CompanySuccessReasonDto>>> Create([FromBody] CreateCompanySuccessReasonDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanySuccessReasons.Update)]
        public override async Task<ActionResult<ApiResponse<CompanySuccessReasonDto>>> Update(Guid id, [FromBody] UpdateCompanySuccessReasonDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanySuccessReasons.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
