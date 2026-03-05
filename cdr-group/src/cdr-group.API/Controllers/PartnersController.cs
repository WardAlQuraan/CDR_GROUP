using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Partner;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace cdr_group.API.Controllers
{
    public class PartnersController : BaseController<PartnerDto, CreatePartnerDto, UpdatePartnerDto, IPartnerService>
    {
        protected override string EntityName => "Partner";

        public PartnersController(IPartnerService partnerService) : base(partnerService)
        {
        }

        [NonAction]
        public override async Task<ActionResult<ApiResponse<PagedResult<PartnerDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<PartnerDto>>>> GetPartnersPaged([FromQuery] PartnerPagedRequest request)
        {
            var result = await Service.GetPartnersPagedAsync(request);
            return Ok(ApiResponse<PagedResult<PartnerDto>>.SuccessResponse(result));
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<IEnumerable<PartnerDto>>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<PartnerDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        [HasPermission(Permissions.Partners.Create)]
        public override async Task<ActionResult<ApiResponse<PartnerDto>>> Create([FromBody] CreatePartnerDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Partners.Update)]
        public override async Task<ActionResult<ApiResponse<PartnerDto>>> Update(Guid id, [FromBody] UpdatePartnerDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Partners.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
