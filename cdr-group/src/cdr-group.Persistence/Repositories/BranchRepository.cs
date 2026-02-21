using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Branch?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(b => b.Company)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<Branch?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .Include(b => b.Company)
                .FirstOrDefaultAsync(b => b.Code == code && !b.IsDeleted);
        }

        public async Task<IEnumerable<Branch>> GetActiveAsync()
        {
            return await _dbSet
                .Include(b => b.Company)
                .Where(b => b.IsActive && !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Branch> Items, int TotalCount)> GetBranchesPagedAsync(PagedRequest request)
        {
            var query = _dbSet
                .Include(b => b.Company)
                .Where(b => !b.IsDeleted);

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, b => b.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> BranchCodeExistsAsync(string code, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(b =>
                b.Code == code &&
                !b.IsDeleted &&
                (excludeId == null || b.Id != excludeId));
        }

        public async Task<IEnumerable<Branch>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _dbSet
                .Include(b => b.Company)
                .Where(b => b.CompanyId == companyId && !b.IsDeleted)
                .ToListAsync();
        }
    }
}
