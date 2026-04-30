using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyBranch;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompanyBranchesController : BaseController<CompanyBranchDto, CreateCompanyBranchDto, UpdateCompanyBranchDto, ICompanyBranchService>
    {
        protected override string EntityName => "CompanyBranch";

        public CompanyBranchesController(ICompanyBranchService companyBranchService) : base(companyBranchService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyBranchDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyBranchDto>>>> GetCompanyBranchesPaged([FromQuery] CompanyBranchPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyBranchDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyBranchDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyBranchDto>>>> GetByCompany(Guid companyId)
        {
            var branches = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyBranchDto>>.SuccessResponse(branches));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyBranches.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyBranchDto>>> Create([FromBody] CreateCompanyBranchDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyBranches.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyBranchDto>>> Update(Guid id, [FromBody] UpdateCompanyBranchDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyBranches.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
