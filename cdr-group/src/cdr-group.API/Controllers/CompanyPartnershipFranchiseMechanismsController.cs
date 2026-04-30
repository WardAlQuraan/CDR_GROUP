using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyPartnershipFranchiseMechanism;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompanyPartnershipFranchiseMechanismsController : BaseController<CompanyPartnershipFranchiseMechanismDto, CreateCompanyPartnershipFranchiseMechanismDto, UpdateCompanyPartnershipFranchiseMechanismDto, ICompanyPartnershipFranchiseMechanismService>
    {
        protected override string EntityName => "CompanyPartnershipFranchiseMechanism";

        public CompanyPartnershipFranchiseMechanismsController(ICompanyPartnershipFranchiseMechanismService companyPartnershipFranchiseMechanismService) : base(companyPartnershipFranchiseMechanismService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyPartnershipFranchiseMechanismDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyPartnershipFranchiseMechanismDto>>>> GetCompanyPartnershipFranchiseMechanismsPaged([FromQuery] CompanyPartnershipFranchiseMechanismPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyPartnershipFranchiseMechanismDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyPartnershipFranchiseMechanismDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyPartnershipFranchiseMechanismDto>>>> GetByCompany(Guid companyId)
        {
            var mechanisms = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyPartnershipFranchiseMechanismDto>>.SuccessResponse(mechanisms));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyPartnershipFranchiseMechanisms.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyPartnershipFranchiseMechanismDto>>> Create([FromBody] CreateCompanyPartnershipFranchiseMechanismDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyPartnershipFranchiseMechanisms.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyPartnershipFranchiseMechanismDto>>> Update(Guid id, [FromBody] UpdateCompanyPartnershipFranchiseMechanismDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyPartnershipFranchiseMechanisms.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
