using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cdr_group.Contracts.DTOs.Auth;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Services;

namespace cdr_group.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<TokenDto>>> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(ApiResponse<TokenDto>.SuccessResponse(result, "Login successful."));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<TokenDto>>> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(ApiResponse<TokenDto>.SuccessResponse(result, "Registration successful."));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<TokenDto>>> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            var result = await _authService.RefreshTokenAsync(dto.RefreshToken);
            return Ok(ApiResponse<TokenDto>.SuccessResponse(result, "Token refreshed successfully."));
        }

        [HttpPost("revoke-token")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> RevokeToken([FromBody] RefreshTokenRequestDto dto)
        {
            await _authService.RevokeTokenAsync(dto.RefreshToken);
            return Ok(ApiResponse.SuccessResponse("Token revoked successfully."));
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserInfoDto>>> GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            var result = await _authService.GetCurrentUserAsync(userId);
            return Ok(ApiResponse<UserInfoDto>.SuccessResponse(result));
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            var userId = GetCurrentUserId();
            await _authService.ChangePasswordAsync(userId, dto);
            return Ok(ApiResponse.SuccessResponse("Password changed successfully."));
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userIdClaim!);
        }
    }
}
