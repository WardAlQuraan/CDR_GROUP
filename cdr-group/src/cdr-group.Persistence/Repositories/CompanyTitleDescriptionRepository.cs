using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.CompanyTitleDescription;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class CompanyTitleDescriptionRepository : Repository<CompanyTitleDescription>, ICompanyTitleDescriptionRepository
    {
        public CompanyTitleDescriptionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CompanyTitleDescription>> GetAllAsync()
        {
            return await _dbSet.AsQueryable().AsNoTracking().Include(e => e.Company).Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<CompanyTitleDescription?> GetWithCompanyAsync(Guid id)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<CompanyTitleDescription>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => e.CompanyId == companyId && !e.IsDeleted)
                .OrderBy(e => e.Code)
                .ToListAsync();
        }

        public async Task<List<CompanyTitleDescription>> GetByCompanyAndCodeAsync(Guid companyId, string code)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => e.CompanyId == companyId && e.Code == code && !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<(IEnumerable<CompanyTitleDescription> Items, int TotalCount)> GetCompanyTitleDescriptionsPagedAsync(CompanyTitleDescriptionPagedRequest request)
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
