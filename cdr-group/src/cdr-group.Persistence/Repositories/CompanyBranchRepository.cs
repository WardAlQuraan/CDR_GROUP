using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.CompanyBranch;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class CompanyBranchRepository : Repository<CompanyBranch>, ICompanyBranchRepository
    {
        public CompanyBranchRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CompanyBranch>> GetAllAsync()
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Include(e => e.City)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<CompanyBranch?> GetWithRelationsAsync(Guid id)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Include(e => e.City)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<CompanyBranch>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Include(e => e.City)
                .Where(e => e.CompanyId == companyId && !e.IsDeleted)
                .OrderByDescending(e => e.OpeningDate)
                .ToListAsync();
        }

        public async Task<(IEnumerable<CompanyBranch> Items, int TotalCount)> GetCompanyBranchesPagedAsync(CompanyBranchPagedRequest request)
        {
            var query = _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Include(e => e.City)
                .Where(e => !e.IsDeleted);

            if (request.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == request.CompanyId.Value);
            }

            if (request.CityId.HasValue)
            {
                query = query.Where(e => e.CityId == request.CityId.Value);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.OpeningDate);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
