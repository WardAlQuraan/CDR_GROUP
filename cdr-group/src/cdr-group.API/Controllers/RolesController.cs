using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class RolesController : BaseController<RoleDto, CreateRoleDto, UpdateRoleDto, IRoleService>
    {
        protected override string EntityName => "Role";

        public RolesController(IRoleService roleService) : base(roleService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.Roles.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<RoleDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Roles.Read)]
        public override async Task<ActionResult<ApiResponse<RoleDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-name/{name}")]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<ActionResult<ApiResponse<RoleDto>>> GetByName(string name)
        {
            var role = await Service.GetByNameAsync(name);
            if (role == null)
            {
                return NotFound(ApiResponse<RoleDto>.FailureResponse("Role not found."));
            }
            return Ok(ApiResponse<RoleDto>.SuccessResponse(role));
        }

        [HttpPost]
        [HasPermission(Permissions.Roles.Manage)]
        public override async Task<ActionResult<ApiResponse<RoleDto>>> Create([FromBody] CreateRoleDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Roles.Manage)]
        public override async Task<ActionResult<ApiResponse<RoleDto>>> Update(Guid id, [FromBody] UpdateRoleDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Roles.Manage)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        [HttpPost("{id:guid}/permissions")]
        [HasPermission(Permissions.Roles.Manage)]
        public async Task<ActionResult<ApiResponse<RoleDto>>> AssignPermissions(Guid id, [FromBody] AssignPermissionsDto dto)
        {
            var role = await Service.AssignPermissionsToRoleAsync(id, dto);
            if (role == null)
            {
                return NotFound(ApiResponse<RoleDto>.FailureResponse("Role not found."));
            }
            return Ok(ApiResponse<RoleDto>.SuccessResponse(role, "Permissions assigned successfully."));
        }

        [HttpDelete("{id:guid}/permissions")]
        [HasPermission(Permissions.Roles.Manage)]
        public async Task<ActionResult<ApiResponse<RoleDto>>> RemovePermissions(Guid id, [FromBody] List<Guid> permissionIds)
        {
            var role = await Service.RemovePermissionsFromRoleAsync(id, permissionIds);
            if (role == null)
            {
                return NotFound(ApiResponse<RoleDto>.FailureResponse("Role not found."));
            }
            return Ok(ApiResponse<RoleDto>.SuccessResponse(role, "Permissions removed successfully."));
        }
    }
}
