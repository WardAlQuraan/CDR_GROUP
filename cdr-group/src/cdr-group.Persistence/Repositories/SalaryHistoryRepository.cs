using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class SalaryHistoryRepository : Repository<SalaryHistory>, ISalaryHistoryRepository
    {
        public SalaryHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SalaryHistory?> GetWithEmployeeAsync(Guid id)
        {
            return await _dbSet
                .Include(sh => sh.Employee)
                .FirstOrDefaultAsync(sh => sh.Id == id && !sh.IsDeleted);
        }

        public async Task<IEnumerable<SalaryHistory>> GetByEmployeeIdAsync(Guid employeeId)
        {
            return await _dbSet
                .Include(sh => sh.Employee)
                .Where(sh => sh.EmployeeId == employeeId && !sh.IsDeleted)
                .OrderByDescending(sh => sh.EffectiveDate)
                .ToListAsync();
        }

        public async Task<(IEnumerable<SalaryHistory> Items, int TotalCount)> GetSalaryHistoriesPagedAsync(PagedRequest request, Guid? employeeId = null)
        {
            var query = _dbSet
                .Include(sh => sh.Employee)
                .Where(sh => !sh.IsDeleted);

            if (employeeId.HasValue)
            {
                query = query.Where(sh => sh.EmployeeId == employeeId.Value);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, sh => sh.EffectiveDate);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
