using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Review;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class ReviewsController : BaseController<ReviewDto, CreateReviewDto, UpdateReviewDto, IReviewService>
    {
        protected override string EntityName => "Review";

        public ReviewsController(IReviewService reviewService) : base(reviewService)
        {
        }

        [NonAction]
        public override Task<ActionResult<ApiResponse<PagedResult<ReviewDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return base.GetPaged(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<ReviewDto>>>> GetReviewsPaged([FromQuery] ReviewPagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<ReviewDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.Reviews.Read)]
        public override async Task<ActionResult<ApiResponse<ReviewDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public override async Task<ActionResult<ApiResponse<ReviewDto>>> Create([FromBody] CreateReviewDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Reviews.Update)]
        public override async Task<ActionResult<ApiResponse<ReviewDto>>> Update(Guid id, [FromBody] UpdateReviewDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Reviews.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
