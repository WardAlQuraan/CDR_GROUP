using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyDistinguish;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompanyDistinguishesController : BaseController<CompanyDistinguishDto, CreateCompanyDistinguishDto, UpdateCompanyDistinguishDto, ICompanyDistinguishService>
    {
        protected override string EntityName => "CompanyDistinguish";

        public CompanyDistinguishesController(ICompanyDistinguishService companyDistinguishService) : base(companyDistinguishService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyDistinguishDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyDistinguishDto>>>> GetCompanyDistinguishesPaged([FromQuery] CompanyDistinguishPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyDistinguishDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyDistinguishDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyDistinguishDto>>>> GetByCompany(Guid companyId)
        {
            var distinguishes = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyDistinguishDto>>.SuccessResponse(distinguishes));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyDistinguishes.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyDistinguishDto>>> Create([FromBody] CreateCompanyDistinguishDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyDistinguishes.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyDistinguishDto>>> Update(Guid id, [FromBody] UpdateCompanyDistinguishDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyDistinguishes.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
