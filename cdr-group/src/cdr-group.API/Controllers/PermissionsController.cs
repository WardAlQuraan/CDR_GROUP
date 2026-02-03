using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class PermissionsController : BaseController<PermissionDto, CreatePermissionDto, UpdatePermissionDto, IPermissionService>
    {
        protected override string EntityName => "Permission";

        public PermissionsController(IPermissionService permissionService) : base(permissionService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.Roles.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<PermissionDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("all")]
        [HasPermission(Permissions.Roles.Read)]
        public override async Task<ActionResult<ApiResponse<IEnumerable<PermissionDto>>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Roles.Read)]
        public override async Task<ActionResult<ApiResponse<PermissionDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-name/{name}")]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<ActionResult<ApiResponse<PermissionDto>>> GetByName(string name)
        {
            var permission = await Service.GetByNameAsync(name);
            if (permission == null)
            {
                return NotFound(ApiResponse<PermissionDto>.FailureResponse("Permission not found."));
            }
            return Ok(ApiResponse<PermissionDto>.SuccessResponse(permission));
        }

        [HttpGet("by-module/{module}")]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PermissionDto>>>> GetByModule(string module)
        {
            var permissions = await Service.GetByModuleAsync(module);
            return Ok(ApiResponse<IEnumerable<PermissionDto>>.SuccessResponse(permissions));
        }

        [HttpGet("by-role/{roleId:guid}")]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PermissionDto>>>> GetByRoleId(Guid roleId)
        {
            var permissions = await Service.GetByRoleIdAsync(roleId);
            return Ok(ApiResponse<IEnumerable<PermissionDto>>.SuccessResponse(permissions));
        }

        [HttpGet("by-user/{userId:guid}")]
        [HasPermission(Permissions.Users.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PermissionDto>>>> GetByUserId(Guid userId)
        {
            var permissions = await Service.GetByUserIdAsync(userId);
            return Ok(ApiResponse<IEnumerable<PermissionDto>>.SuccessResponse(permissions));
        }

        [HttpPost]
        [HasPermission(Permissions.Roles.Manage)]
        public override async Task<ActionResult<ApiResponse<PermissionDto>>> Create([FromBody] CreatePermissionDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Roles.Manage)]
        public override async Task<ActionResult<ApiResponse<PermissionDto>>> Update(Guid id, [FromBody] UpdatePermissionDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Roles.Manage)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
