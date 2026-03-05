using cdr_group.Contracts.DTOs.City;
using cdr_group.Contracts.DTOs.Common;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICityService : IBaseService<CityDto, CreateCityDto, UpdateCityDto>
    {
        Task<PagedResult<CityDto>> GetCitiesPagedAsync(CityPagedRequest request);
    }
}
