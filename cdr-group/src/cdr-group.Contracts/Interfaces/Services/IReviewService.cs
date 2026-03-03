using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Review;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IReviewService : IBaseService<ReviewDto, CreateReviewDto, UpdateReviewDto>
    {
        Task<PagedResult<ReviewDto>> GetPagedAsync(ReviewPagedRequest request);
    }
}
