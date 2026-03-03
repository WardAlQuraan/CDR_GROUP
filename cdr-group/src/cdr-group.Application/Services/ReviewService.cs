using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Review;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class ReviewService : BaseService<Review, ReviewDto, CreateReviewDto, UpdateReviewDto>, IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Review> Repository => UnitOfWork.Reviews;

        public async Task<PagedResult<ReviewDto>> GetPagedAsync(ReviewPagedRequest request)
        {
            var (items, totalCount) = await UnitOfWork.Reviews.GetReviewsPagedAsync(request);
            var dtos = Mapper.Map<List<ReviewDto>>(items);
            return new PagedResult<ReviewDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
