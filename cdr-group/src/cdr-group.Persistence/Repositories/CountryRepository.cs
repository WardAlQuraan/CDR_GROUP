using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace cdr_group.Persistence.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Country>> GetCountriesWithCitiesAsync()
        {
            return await _context.Countries.AsQueryable().AsNoTracking()
                .Where(c => !c.IsDeleted && c.Cities.Any(city => !city.IsDeleted))
                .OrderBy(c => c.NameEn)
                .ToListAsync();
        }
    }
}
