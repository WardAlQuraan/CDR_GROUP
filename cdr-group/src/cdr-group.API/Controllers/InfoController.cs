using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cdr_group.Contracts.DTOs.Common;

namespace cdr_group.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        [HttpGet("ip")]
        [AllowAnonymous]
        public ActionResult<ApiResponse<ClientInfoDto>> GetClientIp()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            // Handle IPv6 loopback (::1) and IPv4 loopback (127.0.0.1)
            if (ipAddress == "::1")
            {
                ipAddress = "127.0.0.1";
            }

            // Check for forwarded IP (when behind proxy/load balancer)
            var forwardedFor = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                // X-Forwarded-For can contain multiple IPs, take the first one (original client)
                ipAddress = forwardedFor.Split(',').First().Trim();
            }

            var clientInfo = new ClientInfoDto
            {
                IpAddress = ipAddress,
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            };

            return Ok(ApiResponse<ClientInfoDto>.SuccessResponse(clientInfo));
        }
    }

    public class ClientInfoDto
    {
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
