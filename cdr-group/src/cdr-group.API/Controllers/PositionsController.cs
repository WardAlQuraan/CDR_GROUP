using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Position;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class PositionsController : BaseController<PositionDto, CreatePositionDto, UpdatePositionDto, IPositionService>
    {
        protected override string EntityName => "Position";

        public PositionsController(IPositionService positionService) : base(positionService)
        {
        }

        [HttpGet]
        [HasPermission(Permissions.Positions.Read)]
        public override async Task<ActionResult<ApiResponse<PagedResult<PositionDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Positions.Read)]
        public override async Task<ActionResult<ApiResponse<PositionDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-code/{code}")]
        [HasPermission(Permissions.Positions.Read)]
        public async Task<ActionResult<ApiResponse<PositionDto>>> GetByCode(string code)
        {
            var position = await Service.GetByCodeAsync(code);
            if (position == null)
            {
                return NotFound(ApiResponse<PositionDto>.FailureResponse("Position not found."));
            }
            return Ok(ApiResponse<PositionDto>.SuccessResponse(position));
        }

        [HttpGet("by-name/{name}")]
        [HasPermission(Permissions.Positions.Read)]
        public async Task<ActionResult<ApiResponse<PositionDto>>> GetByName(string name)
        {
            var position = await Service.GetByNameAsync(name);
            if (position == null)
            {
                return NotFound(ApiResponse<PositionDto>.FailureResponse("Position not found."));
            }
            return Ok(ApiResponse<PositionDto>.SuccessResponse(position));
        }

        [HttpGet("active")]
        [HasPermission(Permissions.Positions.Read)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PositionDto>>>> GetActivePositions()
        {
            var positions = await Service.GetActivePositionsAsync();
            return Ok(ApiResponse<IEnumerable<PositionDto>>.SuccessResponse(positions));
        }

        [HttpGet("{id:guid}/with-employee-count")]
        [HasPermission(Permissions.Positions.Read)]
        public async Task<ActionResult<ApiResponse<PositionWithEmployeesDto>>> GetWithEmployeeCount(Guid id)
        {
            var position = await Service.GetWithEmployeeCountAsync(id);
            if (position == null)
            {
                return NotFound(ApiResponse<PositionWithEmployeesDto>.FailureResponse("Position not found."));
            }
            return Ok(ApiResponse<PositionWithEmployeesDto>.SuccessResponse(position));
        }

        [HttpPost]
        [HasPermission(Permissions.Positions.Create)]
        public override async Task<ActionResult<ApiResponse<PositionDto>>> Create([FromBody] CreatePositionDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Positions.Update)]
        public override async Task<ActionResult<ApiResponse<PositionDto>>> Update(Guid id, [FromBody] UpdatePositionDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Positions.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }

    }
}
