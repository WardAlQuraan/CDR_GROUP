using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Complaint;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class ComplaintsController : BaseController<ComplaintDto, CreateComplaintDto, UpdateComplaintDto, IComplaintService>
    {
        protected override string EntityName => "Complaint";

        public ComplaintsController(IComplaintService complaintService) : base(complaintService)
        {
        }

        [NonAction]
        public override async Task<ActionResult<ApiResponse<PagedResult<ComplaintDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet]
        [HasPermission(Permissions.Complaints.Read)]
        public async Task<ActionResult<ApiResponse<PagedResult<ComplaintDto>>>> GetComplaintsPaged([FromQuery] ComplaintPagedRequest request)
        {
            var result = await Service.GetComplaintsPagedAsync(request);
            return Ok(ApiResponse<PagedResult<ComplaintDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Complaints.Read)]
        public override async Task<ActionResult<ApiResponse<ComplaintDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<ComplaintDto>>> Create([FromBody] CreateComplaintDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Complaints.Update)]
        public override async Task<ActionResult<ApiResponse<ComplaintDto>>> Update(Guid id, [FromBody] UpdateComplaintDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Complaints.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
