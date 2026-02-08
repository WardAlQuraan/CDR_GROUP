using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<ContactUs> Items, int TotalCount)> GetContactUsPagedAsync(PagedRequest request)
        {
            var query = _dbSet.Where(e => !e.IsDeleted);

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
