using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.CompanyPreContractStudy;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class CompanyPreContractStudyRepository : Repository<CompanyPreContractStudy>, ICompanyPreContractStudyRepository
    {
        public CompanyPreContractStudyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CompanyPreContractStudy>> GetAllAsync()
        {
            return await _dbSet.AsQueryable().AsNoTracking().Include(e => e.Company).Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<CompanyPreContractStudy?> GetWithCompanyAsync(Guid id)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<CompanyPreContractStudy>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => e.CompanyId == companyId && !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<(IEnumerable<CompanyPreContractStudy> Items, int TotalCount)> GetCompanyPreContractStudiesPagedAsync(CompanyPreContractStudyPagedRequest request)
        {
            var query = _dbSet.AsQueryable().AsNoTracking()
                .Include(e => e.Company)
                .Where(e => !e.IsDeleted);

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
