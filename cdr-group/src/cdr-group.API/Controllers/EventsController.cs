using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Event;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class EventsController : BaseController<EventDto, CreateEventDto, UpdateEventDto, IEventService>
    {
        protected override string EntityName => "Event";

        public EventsController(IEventService eventService) : base(eventService)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<PagedResult<EventDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<EventDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-department/{departmentId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<EventDto>>>> GetByDepartment(Guid departmentId)
        {
            var events = await Service.GetByDepartmentIdAsync(departmentId);
            return Ok(ApiResponse<IEnumerable<EventDto>>.SuccessResponse(events));
        }

        [HttpPost]
        [HasPermission(Permissions.Events.Create)]
        public override async Task<ActionResult<ApiResponse<EventDto>>> Create([FromBody] CreateEventDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Events.Update)]
        public override async Task<ActionResult<ApiResponse<EventDto>>> Update(Guid id, [FromBody] UpdateEventDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Events.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
