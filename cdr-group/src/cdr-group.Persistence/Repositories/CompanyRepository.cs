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

        public async Task<Company?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Code == code && !c.IsDeleted);
        }

        public async Task<IEnumerable<Company>> GetActiveCompaniesAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Company> Items, int TotalCount)> GetCompaniesPagedAsync(PagedRequest request)
        {
            var query = _dbSet.Where(c => !c.IsDeleted);

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, c => c.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> CompanyCodeExistsAsync(string code, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(c =>
                c.Code == code &&
                !c.IsDeleted &&
                (excludeId == null || c.Id != excludeId));
        }

        public async Task<bool> HasDepartmentsAsync(Guid companyId)
        {
            return await _context.Departments.AnyAsync(d =>
                d.CompanyId == companyId && !d.IsDeleted);
        }
    }
}
