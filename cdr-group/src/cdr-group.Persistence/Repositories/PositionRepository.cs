using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Position?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.Code == code && !p.IsDeleted);
        }

        public async Task<Position?> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => (p.NameEn == name || p.NameAr == name) && !p.IsDeleted);
        }

        public async Task<IEnumerable<Position>> GetActivePositionsAsync()
        {
            return await _dbSet
                .Where(p => p.IsActive && !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Position> Items, int TotalCount)> GetPositionsPagedAsync(PagedRequest request)
        {
            var query = _dbSet
                .Where(p => !p.IsDeleted);

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, p => p.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> PositionCodeExistsAsync(string code, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(p =>
                p.Code == code &&
                !p.IsDeleted &&
                (excludeId == null || p.Id != excludeId));
        }

        public async Task<bool> HasEmployeesAsync(Guid positionId)
        {
            return await _context.Employees.AnyAsync(e =>
                e.PositionId == positionId && !e.IsDeleted);
        }

        public async Task<int> GetEmployeeCountAsync(Guid positionId)
        {
            return await _context.Employees.CountAsync(e =>
                e.PositionId == positionId && !e.IsDeleted);
        }
    }
}
