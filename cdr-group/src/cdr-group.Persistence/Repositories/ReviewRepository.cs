using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Review;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Review> Items, int TotalCount)> GetReviewsPagedAsync(ReviewPagedRequest request)
        {
            var query = _dbSet.Include(e => e.Company).Where(e => !e.IsDeleted);

            if (request.IsVisible.HasValue)
            {
                query = query.Where(e => e.IsVisible == request.IsVisible.Value);
            }

            if (request.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == request.CompanyId.Value);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
