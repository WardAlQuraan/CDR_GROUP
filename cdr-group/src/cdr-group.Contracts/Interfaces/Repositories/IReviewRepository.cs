using cdr_group.Contracts.DTOs.Review;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<(IEnumerable<Review> Items, int TotalCount)> GetReviewsPagedAsync(ReviewPagedRequest request);
    }
}
