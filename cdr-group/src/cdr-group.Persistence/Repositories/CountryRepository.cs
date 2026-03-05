using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;

namespace cdr_group.Persistence.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
