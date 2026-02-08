using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Company;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CompaniesController : BaseController<CompanyDto, CreateCompanyDto, UpdateCompanyDto, ICompanyService>
    {
        protected override string EntityName => "Company";

        public CompaniesController(ICompanyService companyService) : base(companyService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.Companies.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<CompanyDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Companies.Read)]
        public override async Task<ActionResult<ApiResponse<CompanyDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-code/{code}")]
        [HasPermission(Permissions.Companies.Read)]
        public async Task<ActionResult<ApiResponse<CompanyDto>>> GetByCode(string code)
        {
            var company = await Service.GetByCodeAsync(code);
            if (company == null)
            {
                return NotFound(ApiResponse<CompanyDto>.FailureResponse("Company not found."));
            }
            return Ok(ApiResponse<CompanyDto>.SuccessResponse(company));
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyDto>>>> GetActiveCompanies()
        {
            var companies = await Service.GetActiveCompaniesAsync();
            return Ok(ApiResponse<IEnumerable<CompanyDto>>.SuccessResponse(companies));
        }

        [HttpPost]
        [HasPermission(Permissions.Companies.Create)]
        public override async Task<ActionResult<ApiResponse<CompanyDto>>> Create([FromBody] CreateCompanyDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Companies.Update)]
        public override async Task<ActionResult<ApiResponse<CompanyDto>>> Update(Guid id, [FromBody] UpdateCompanyDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Companies.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
