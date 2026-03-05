using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.City;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class CitiesController : BaseController<CityDto, CreateCityDto, UpdateCityDto, ICityService>
    {
        protected override string EntityName => "City";

        public CitiesController(ICityService cityService) : base(cityService)
        {
        }

        [NonAction]
        public override async Task<ActionResult<ApiResponse<PagedResult<CityDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<CityDto>>>> GetCitiesPaged([FromQuery] CityPagedRequest request)
        {
            var result = await Service.GetCitiesPagedAsync(request);
            return Ok(ApiResponse<PagedResult<CityDto>>.SuccessResponse(result));
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<IEnumerable<CityDto>>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CityDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        [HasPermission(Permissions.Cities.Create)]
        public override async Task<ActionResult<ApiResponse<CityDto>>> Create([FromBody] CreateCityDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Cities.Update)]
        public override async Task<ActionResult<ApiResponse<CityDto>>> Update(Guid id, [FromBody] UpdateCityDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Cities.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
