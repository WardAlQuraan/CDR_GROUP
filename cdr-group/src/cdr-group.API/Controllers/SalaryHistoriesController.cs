using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.SalaryHistory;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class SalaryHistoriesController : BaseController<SalaryHistoryDto, CreateSalaryHistoryDto, UpdateSalaryHistoryDto, ISalaryHistoryService>
    {
        protected override string EntityName => "SalaryHistory";

        public SalaryHistoriesController(ISalaryHistoryService salaryHistoryService) : base(salaryHistoryService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.SalaryHistories.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<SalaryHistoryDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.SalaryHistories.Read)]
        public override async Task<ActionResult<ApiResponse<SalaryHistoryDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-employee/{employeeId:guid}")]
        [HasPermission(Permissions.SalaryHistories.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SalaryHistoryDto>>>> GetByEmployee(Guid employeeId)
        {
            var items = await Service.GetByEmployeeIdAsync(employeeId);
            var res = ApiResponse<IEnumerable<SalaryHistoryDto>>.SuccessResponse(items);
            return Ok(res);
        }

        [HttpPost]
        [HasPermission(Permissions.SalaryHistories.Create)]
        public override async Task<ActionResult<ApiResponse<SalaryHistoryDto>>> Create([FromBody] CreateSalaryHistoryDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.SalaryHistories.Update)]
        public override async Task<ActionResult<ApiResponse<SalaryHistoryDto>>> Update(Guid id, [FromBody] UpdateSalaryHistoryDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.SalaryHistories.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
