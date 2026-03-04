using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Complaint;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class ComplaintRepository : Repository<Complaint>, IComplaintRepository
    {
        public ComplaintRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Complaint>> GetAllAsync()
        {
            return await _dbSet.Include(e => e.Company).Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<(IEnumerable<Complaint> Items, int TotalCount)> GetComplaintsPagedAsync(ComplaintPagedRequest request)
        {
            var query = _dbSet.Include(e => e.Company).Where(e => !e.IsDeleted);

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
