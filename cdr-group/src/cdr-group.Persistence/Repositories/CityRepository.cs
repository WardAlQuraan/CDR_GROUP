using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.City;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _dbSet.Include(e => e.Country).Where(e => !e.IsDeleted).ToListAsync();
        }

        public override async Task<City?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(e => e.Country).FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<(IEnumerable<City> Items, int TotalCount)> GetCitiesPagedAsync(CityPagedRequest request)
        {
            var query = _dbSet.Include(e => e.Country).Where(e => !e.IsDeleted);

            if (request.CountryId.HasValue)
            {
                query = query.Where(e => e.CountryId == request.CountryId.Value);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
