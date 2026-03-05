using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Country;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cdr_group.API.Controllers
{
    public class CountriesController : BaseController<CountryDto, CreateCountryDto, UpdateCountryDto, ICountryService>
    {
        protected override string EntityName => "Country";

        public CountriesController(ICountryService countryService) : base(countryService)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<PagedResult<CountryDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<IEnumerable<CountryDto>>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<CountryDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        [HasPermission(Permissions.Countries.Create)]
        public override async Task<ActionResult<ApiResponse<CountryDto>>> Create([FromBody] CreateCountryDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Countries.Update)]
        public override async Task<ActionResult<ApiResponse<CountryDto>>> Update(Guid id, [FromBody] UpdateCountryDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Countries.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
