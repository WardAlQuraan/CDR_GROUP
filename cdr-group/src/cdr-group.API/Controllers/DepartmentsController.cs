using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Department;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class DepartmentsController : BaseController<DepartmentDto, CreateDepartmentDto, UpdateDepartmentDto, IDepartmentService>
    {
        protected override string EntityName => "Department";

        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.Departments.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<DepartmentDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Departments.Read)]
        public override async Task<ActionResult<ApiResponse<DepartmentDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-code/{code}")]
        [HasPermission(Permissions.Departments.Read)]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> GetByCode(string code)
        {
            var department = await Service.GetByCodeAsync(code);
            if (department == null)
            {
                return NotFound(ApiResponse<DepartmentDto>.FailureResponse("Department not found."));
            }
            return Ok(ApiResponse<DepartmentDto>.SuccessResponse(department));
        }

        [HttpGet("by-name/{name}")]
        [HasPermission(Permissions.Departments.Read)]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> GetByName(string name)
        {
            var department = await Service.GetByNameAsync(name);
            if (department == null)
            {
                return NotFound(ApiResponse<DepartmentDto>.FailureResponse("Department not found."));
            }
            return Ok(ApiResponse<DepartmentDto>.SuccessResponse(department));
        }

        [HttpGet("{id:guid}/with-sub-departments")]
        [HasPermission(Permissions.Departments.Read)]
        public async Task<ActionResult<ApiResponse<DepartmentWithSubDepartmentsDto>>> GetWithSubDepartments(Guid id)
        {
            var department = await Service.GetWithSubDepartmentsAsync(id);
            if (department == null)
            {
                return NotFound(ApiResponse<DepartmentWithSubDepartmentsDto>.FailureResponse("Department not found."));
            }
            return Ok(ApiResponse<DepartmentWithSubDepartmentsDto>.SuccessResponse(department));
        }

        [HttpGet("{id:guid}/sub-departments")]
        [HasPermission(Permissions.Departments.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<DepartmentBasicDto>>>> GetSubDepartments(Guid id)
        {
            var subDepartments = await Service.GetSubDepartmentsAsync(id);
            return Ok(ApiResponse<IEnumerable<DepartmentBasicDto>>.SuccessResponse(subDepartments));
        }

        [HttpGet("root")]
        [HasPermission(Permissions.Departments.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<DepartmentDto>>>> GetRootDepartments()
        {
            var departments = await Service.GetRootDepartmentsAsync();
            return Ok(ApiResponse<IEnumerable<DepartmentDto>>.SuccessResponse(departments));
        }

        [HttpGet("active")]
        [HasPermission(Permissions.Departments.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<DepartmentDto>>>> GetActiveDepartments()
        {
            var departments = await Service.GetActiveDepartmentsAsync();
            return Ok(ApiResponse<IEnumerable<DepartmentDto>>.SuccessResponse(departments));
        }

        [HttpPost]
        [HasPermission(Permissions.Departments.Create)]
        public override async Task<ActionResult<ApiResponse<DepartmentDto>>> Create([FromBody] CreateDepartmentDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Departments.Update)]
        public override async Task<ActionResult<ApiResponse<DepartmentDto>>> Update(Guid id, [FromBody] UpdateDepartmentDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Departments.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        [HttpPut("{id:guid}/manager")]
        [HasPermission(Permissions.Departments.AssignManager)]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> AssignManager(Guid id, [FromBody] Guid? managerId)
        {
            var department = await Service.AssignManagerAsync(id, managerId);
            if (department == null)
            {
                return NotFound(ApiResponse<DepartmentDto>.FailureResponse("Department not found."));
            }
            return Ok(ApiResponse<DepartmentDto>.SuccessResponse(department, "Manager assigned successfully."));
        }
    }
}
