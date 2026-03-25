using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Company?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<Company>> GetActiveCompaniesAsync()
        {
            return await _dbSet
                .Include(c => c.Parent)
                .Include(c => c.Children.Where(ch => !ch.IsDeleted))
                .Where(c => c.IsActive && !c.IsDeleted)
                .OrderBy(x=>x.CreatedAt)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Company> Items, int TotalCount)> GetCompaniesPagedAsync(PagedRequest request)
        {
            var query = _dbSet
            .Include(c => c.Parent)
            .Include(c => c.Children.Where(ch => !ch.IsDeleted))
            .Where(c => !c.IsDeleted);

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, c => c.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> HasEmployeesAsync(Guid companyId)
        {
            return await _context.Employees.AnyAsync(e =>
                e.CompanyId == companyId && !e.IsDeleted);
        }

        public async Task<bool> HasChildrenAsync(Guid companyId)
        {
            return await _dbSet.AnyAsync(c =>
                c.ParentId == companyId && !c.IsDeleted);
        }

        public async Task<bool> HasActiveChildrenAsync(Guid companyId)
        {
            return await _dbSet.AnyAsync(c =>
                c.ParentId == companyId && c.IsActive && !c.IsDeleted);
        }

        public async Task<Dictionary<Guid, int>> GetPartnersCountAsync(IEnumerable<Guid> companyIds)
        {
            var ids = companyIds.ToList();
            return await _context.Partners
                .Where(p => ids.Contains(p.CompanyId) && p.Status == Domain.Enums.PartnerStatus.Present && !p.IsDeleted)
                .GroupBy(p => p.CompanyId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

       
    }
}
