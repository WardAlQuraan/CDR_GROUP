using cdr_group.Contracts.DTOs.City;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<(IEnumerable<City> Items, int TotalCount)> GetCitiesPagedAsync(CityPagedRequest request);
    }
}
