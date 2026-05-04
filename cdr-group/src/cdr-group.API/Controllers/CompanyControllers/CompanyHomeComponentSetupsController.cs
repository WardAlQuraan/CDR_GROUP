using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyHomeComponentSetup;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers.CompanyControllers
{
    public class CompanyHomeComponentSetupsController : BaseController<CompanyHomeComponentSetupDto, CreateCompanyHomeComponentSetupDto, UpdateCompanyHomeComponentSetupDto, ICompanyHomeComponentSetupService>
    {
        protected override string EntityName => "CompanyHomeComponentSetup";

        public CompanyHomeComponentSetupsController(ICompanyHomeComponentSetupService companyHomeComponentSetupService) : base(companyHomeComponentSetupService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyHomeComponentSetupDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyHomeComponentSetupDto>>>> GetCompanyHomeComponentSetupsPaged([FromQuery] CompanyHomeComponentSetupPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyHomeComponentSetupDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyHomeComponentSetupDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyHomeComponentSetupDto>>>> GetByCompany(Guid companyId)
        {
            var items = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyHomeComponentSetupDto>>.SuccessResponse(items));
        }

        [HttpGet("by-company/{companyId:guid}/component/{componentCode}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<CompanyHomeComponentSetupDto>>> GetByCompanyAndComponent(Guid companyId, string componentCode)
        {
            var item = await Service.GetByCompanyAndComponentCodeAsync(companyId, componentCode);
            if (item == null)
            {
                return NotFound(ApiResponse<CompanyHomeComponentSetupDto>.FailureResponse($"{EntityName} not found."));
            }
            return Ok(ApiResponse<CompanyHomeComponentSetupDto>.SuccessResponse(item));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyHomeComponentSetups.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyHomeComponentSetupDto>>> Create([FromBody] CreateCompanyHomeComponentSetupDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyHomeComponentSetups.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyHomeComponentSetupDto>>> Update(Guid id, [FromBody] UpdateCompanyHomeComponentSetupDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyHomeComponentSetups.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
