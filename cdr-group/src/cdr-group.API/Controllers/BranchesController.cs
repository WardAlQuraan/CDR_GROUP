using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Branch;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class BranchesController : BaseController<BranchDto, CreateBranchDto, UpdateBranchDto, IBranchService>
    {
        protected override string EntityName => "Branch";

        public BranchesController(IBranchService branchService) : base(branchService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.Branches.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<BranchDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Branches.Read)]
        public override async Task<ActionResult<ApiResponse<BranchDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-code/{code}")]
        [HasPermission(Permissions.Branches.Read)]
        public async Task<ActionResult<ApiResponse<BranchDto>>> GetByCode(string code)
        {
            var branch = await Service.GetByCodeAsync(code);
            if (branch == null)
            {
                return NotFound(ApiResponse<BranchDto>.FailureResponse("Branch not found."));
            }
            return Ok(ApiResponse<BranchDto>.SuccessResponse(branch));
        }

        [HttpGet("by-company/{companyId:guid}")]
        [HasPermission(Permissions.Branches.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<BranchDto>>>> GetByCompanyId(Guid companyId)
        {
            var branches = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<BranchDto>>.SuccessResponse(branches));
        }

        [HttpGet("active")]
        [HasPermission(Permissions.Branches.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<BranchDto>>>> GetActive()
        {
            var branches = await Service.GetActiveAsync();
            return Ok(ApiResponse<IEnumerable<BranchDto>>.SuccessResponse(branches));
        }

        [HttpPost]
        [HasPermission(Permissions.Branches.Create)]
        public override async Task<ActionResult<ApiResponse<BranchDto>>> Create([FromBody] CreateBranchDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Branches.Update)]
        public override async Task<ActionResult<ApiResponse<BranchDto>>> Update(Guid id, [FromBody] UpdateBranchDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Branches.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
