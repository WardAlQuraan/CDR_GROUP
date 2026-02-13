using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Employee;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class EmployeesController : BaseController<EmployeeDto, CreateEmployeeDto, UpdateEmployeeDto, IEmployeeService>
    {
        protected override string EntityName => "Employee";

        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
        }

        [HttpGet]
        public override async Task<ActionResult<ApiResponse<PagedResult<EmployeeDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        public override async Task<ActionResult<ApiResponse<EmployeeDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("{id:guid}/with-subordinates")]
        public async Task<ActionResult<ApiResponse<EmployeeWithSubordinatesDto>>> GetWithSubordinates(Guid id)
        {
            var employee = await Service.GetWithSubordinatesAsync(id);
            if (employee == null)
            {
                return NotFound(ApiResponse<EmployeeWithSubordinatesDto>.FailureResponse("Employee not found."));
            }
            return Ok(ApiResponse<EmployeeWithSubordinatesDto>.SuccessResponse(employee));
        }

        [HttpGet("by-code/{employeeCode}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetByEmployeeCode(string employeeCode)
        {
            var employee = await Service.GetByEmployeeCodeAsync(employeeCode);
            if (employee == null)
            {
                return NotFound(ApiResponse<EmployeeDto>.FailureResponse("Employee not found."));
            }
            return Ok(ApiResponse<EmployeeDto>.SuccessResponse(employee));
        }

        [HttpGet("by-user/{userId:guid}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetByUserId(Guid userId)
        {
            var employee = await Service.GetByUserIdAsync(userId);
            if (employee == null)
            {
                return NotFound(ApiResponse<EmployeeDto>.FailureResponse("Employee not found."));
            }
            return Ok(ApiResponse<EmployeeDto>.SuccessResponse(employee));
        }

        [HttpGet("{id:guid}/subordinates")]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeeBasicDto>>>> GetSubordinates(Guid id)
        {
            var subordinates = await Service.GetSubordinatesAsync(id);
            return Ok(ApiResponse<IEnumerable<EmployeeBasicDto>>.SuccessResponse(subordinates));
        }

        [HttpGet("by-company")]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeeDto>>>> GetByCompanyId([FromQuery]Guid? companyId)
        {
            var employees = await Service.GetByCompanyIdAsync(companyId);
            return Ok(ApiResponse<IEnumerable<EmployeeDto>>.SuccessResponse(employees));
        }

        [HttpGet("tree")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeeTreeNodeDto>>>> GetTree([FromQuery] GetTreeRequest request)
        {
            var tree = await Service.GetTreeAsync(request);
            return Ok(ApiResponse<IEnumerable<EmployeeTreeNodeDto>>.SuccessResponse(tree));
        }

        [HttpPost]
        [HasPermission(Permissions.Employees.Create)]
        public override async Task<ActionResult<ApiResponse<EmployeeDto>>> Create([FromBody] CreateEmployeeDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Employees.Update)]
        public override async Task<ActionResult<ApiResponse<EmployeeDto>>> Update(Guid id, [FromBody] UpdateEmployeeDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Employees.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        [HttpPut("{id:guid}/manager")]
        [HasPermission(Permissions.Employees.AssignManager)]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> AssignManager(Guid id, [FromBody] Guid? managerId)
        {
            var employee = await Service.AssignManagerAsync(id, managerId);
            if (employee == null)
            {
                return NotFound(ApiResponse<EmployeeDto>.FailureResponse("Employee not found."));
            }
            return Ok(ApiResponse<EmployeeDto>.SuccessResponse(employee, "Manager assigned successfully."));
        }

        [HttpPut("{id:guid}/user")]
        [HasPermission(Permissions.Employees.LinkToUser)]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> LinkToUser(Guid id, [FromBody] Guid? userId)
        {
            var employee = await Service.LinkToUserAsync(id, userId);
            if (employee == null)
            {
                return NotFound(ApiResponse<EmployeeDto>.FailureResponse("Employee not found."));
            }
            return Ok(ApiResponse<EmployeeDto>.SuccessResponse(employee, "Employee linked to user successfully."));
        }

    }
}
