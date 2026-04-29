using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.CompanyPreference;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class CompanyPreferenceRepository : Repository<CompanyPreference>, ICompanyPreferenceRepository
    {
        public CompanyPreferenceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CompanyPreference>> GetAllAsync()
        {
            return await _dbSet.AsQueryable().AsNoTracking().Include(e => e.Company).Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<CompanyPreference?> GetWithCompanyAsync(Guid id)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<CompanyPreference>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => e.CompanyId == companyId && !e.IsDeleted)
                .OrderBy(e => e.Code)
                .ToListAsync();
        }

        public async Task<CompanyPreference?> GetByCompanyAndCodeAsync(Guid companyId, string code)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Code == code && !e.IsDeleted);
        }

        public async Task<(IEnumerable<CompanyPreference> Items, int TotalCount)> GetCompanyPreferencesPagedAsync(CompanyPreferencePagedRequest request)
        {
            var query = _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => !e.IsDeleted);

            if (request.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == request.CompanyId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                query = query.Where(e => e.Code == request.Code);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.Code);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
