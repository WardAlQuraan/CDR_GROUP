using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class UsersController : BaseController<UserDto, CreateUserDto, UpdateUserDto, IUserService>
    {
        protected override string EntityName => "User";

        public UsersController(IUserService userService) : base(userService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.Users.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<UserDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Users.Read)]
        public override async Task<ActionResult<ApiResponse<UserDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-username/{username}")]
        [HasPermission(Permissions.Users.Read)]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetByUsername(string username)
        {
            var user = await Service.GetByUsernameAsync(username);
            if (user == null)
            {
                return NotFound(ApiResponse<UserDto>.FailureResponse("User not found."));
            }
            return Ok(ApiResponse<UserDto>.SuccessResponse(user));
        }

        [HttpGet("by-email/{email}")]
        [HasPermission(Permissions.Users.Read)]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetByEmail(string email)
        {
            var user = await Service.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound(ApiResponse<UserDto>.FailureResponse("User not found."));
            }
            return Ok(ApiResponse<UserDto>.SuccessResponse(user));
        }

        [HttpPost]
        [HasPermission(Permissions.Users.Create)]
        public override async Task<ActionResult<ApiResponse<UserDto>>> Create([FromBody] CreateUserDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Users.Update)]
        public override async Task<ActionResult<ApiResponse<UserDto>>> Update(Guid id, [FromBody] UpdateUserDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Users.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        [HttpPost("{id:guid}/change-password")]
        [HasPermission(Permissions.Users.Update)]
        public async Task<ActionResult<ApiResponse>> ChangePassword(Guid id, [FromBody] ChangePasswordDto dto)
        {
            var result = await Service.ChangePasswordAsync(id, dto);
            if (!result)
            {
                return NotFound(ApiResponse.FailureResponse("User not found."));
            }
            return Ok(ApiResponse.SuccessResponse("Password changed successfully."));
        }

        [HttpPost("{id:guid}/roles")]
        [HasPermission(Permissions.Users.Update)]
        public async Task<ActionResult<ApiResponse<UserDto>>> AssignRoles(Guid id, [FromBody] AssignRolesDto dto)
        {
            var user = await Service.AssignRolesToUserAsync(id, dto);
            if (user == null)
            {
                return NotFound(ApiResponse<UserDto>.FailureResponse("User not found."));
            }
            return Ok(ApiResponse<UserDto>.SuccessResponse(user, "Roles assigned successfully."));
        }

        [HttpDelete("{id:guid}/roles")]
        [HasPermission(Permissions.Users.Update)]
        public async Task<ActionResult<ApiResponse<UserDto>>> RemoveRoles(Guid id, [FromBody] List<Guid> roleIds)
        {
            var user = await Service.RemoveRolesFromUserAsync(id, roleIds);
            if (user == null)
            {
                return NotFound(ApiResponse<UserDto>.FailureResponse("User not found."));
            }
            return Ok(ApiResponse<UserDto>.SuccessResponse(user, "Roles removed successfully."));
        }

        [HttpPost("{id:guid}/activate")]
        [HasPermission(Permissions.Users.Activate)]
        public async Task<ActionResult<ApiResponse>> Activate(Guid id)
        {
            var result = await Service.ActivateUserAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse.FailureResponse("User not found."));
            }
            return Ok(ApiResponse.SuccessResponse("User activated successfully."));
        }

        [HttpPost("{id:guid}/deactivate")]
        [HasPermission(Permissions.Users.Activate)]
        public async Task<ActionResult<ApiResponse>> Deactivate(Guid id)
        {
            var result = await Service.DeactivateUserAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse.FailureResponse("User not found."));
            }
            return Ok(ApiResponse.SuccessResponse("User deactivated successfully."));
        }
    }
}
