using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyDistributionMarketing;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompanyDistributionMarketingsController : BaseController<CompanyDistributionMarketingDto, CreateCompanyDistributionMarketingDto, UpdateCompanyDistributionMarketingDto, ICompanyDistributionMarketingService>
    {
        protected override string EntityName => "CompanyDistributionMarketing";

        public CompanyDistributionMarketingsController(ICompanyDistributionMarketingService companyDistributionMarketingService) : base(companyDistributionMarketingService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyDistributionMarketingDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyDistributionMarketingDto>>>> GetCompanyDistributionMarketingsPaged([FromQuery] CompanyDistributionMarketingPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyDistributionMarketingDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyDistributionMarketingDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyDistributionMarketingDto>>>> GetByCompany(Guid companyId)
        {
            var marketings = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyDistributionMarketingDto>>.SuccessResponse(marketings));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyDistributionMarketings.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyDistributionMarketingDto>>> Create([FromBody] CreateCompanyDistributionMarketingDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyDistributionMarketings.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyDistributionMarketingDto>>> Update(Guid id, [FromBody] UpdateCompanyDistributionMarketingDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyDistributionMarketings.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
