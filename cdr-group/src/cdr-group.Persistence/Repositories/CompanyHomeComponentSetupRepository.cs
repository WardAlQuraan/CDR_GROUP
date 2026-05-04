using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.CompanyHomeComponentSetup;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class CompanyHomeComponentSetupRepository : Repository<CompanyHomeComponentSetup>, ICompanyHomeComponentSetupRepository
    {
        public CompanyHomeComponentSetupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CompanyHomeComponentSetup>> GetAllAsync()
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<CompanyHomeComponentSetup?> GetWithCompanyAsync(Guid id)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<CompanyHomeComponentSetup>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => e.CompanyId == companyId && !e.IsDeleted)
                .OrderBy(e => e.Rank)
                .ToListAsync();
        }

        public async Task<CompanyHomeComponentSetup?> GetByCompanyAndComponentCodeAsync(Guid companyId, string componentCode)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.ComponentCode == componentCode && !e.IsDeleted);
        }

        public async Task<(IEnumerable<CompanyHomeComponentSetup> Items, int TotalCount)> GetCompanyHomeComponentSetupsPagedAsync(CompanyHomeComponentSetupPagedRequest request)
        {
            var query = _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => !e.IsDeleted);

            if (request.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == request.CompanyId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.ComponentCode))
            {
                query = query.Where(e => e.ComponentCode == request.ComponentCode);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.Rank);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
