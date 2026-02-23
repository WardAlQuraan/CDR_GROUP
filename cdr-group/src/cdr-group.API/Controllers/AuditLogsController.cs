using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cdr_group.Contracts.DTOs.AuditLog;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogsController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet("{entityName}/{entityId}")]
        [HasPermission(Permissions.AuditLogs.Read)]
        public async Task<ActionResult<ApiResponse<PagedResult<AuditLogDto>>>> GetPaged(
            string entityName,
            string entityId,
            [FromQuery] PagedRequest request
             )
        {
            var result = await _auditLogService.GetPagedAsync(request, entityName, entityId);
            return Ok(ApiResponse<PagedResult<AuditLogDto>>.SuccessResponse(result));
        }
    }
}
