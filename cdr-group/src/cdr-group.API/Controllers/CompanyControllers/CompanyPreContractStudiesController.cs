using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyPreContractStudy;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers.CompanyControllers
{
    public class CompanyPreContractStudiesController : BaseController<CompanyPreContractStudyDto, CreateCompanyPreContractStudyDto, UpdateCompanyPreContractStudyDto, ICompanyPreContractStudyService>
    {
        protected override string EntityName => "CompanyPreContractStudy";

        public CompanyPreContractStudiesController(ICompanyPreContractStudyService companyPreContractStudyService) : base(companyPreContractStudyService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<CompanyPreContractStudyDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyPreContractStudyDto>>>> GetCompanyPreContractStudiesPaged([FromQuery] CompanyPreContractStudyPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CompanyPreContractStudyDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CompanyPreContractStudyDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-company/{companyId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyPreContractStudyDto>>>> GetByCompany(Guid companyId)
        {
            var studies = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<CompanyPreContractStudyDto>>.SuccessResponse(studies));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyPreContractStudies.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyPreContractStudyDto>>> Create([FromBody] CreateCompanyPreContractStudyDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CompanyPreContractStudies.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyPreContractStudyDto>>> Update(Guid id, [FromBody] UpdateCompanyPreContractStudyDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CompanyPreContractStudies.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
