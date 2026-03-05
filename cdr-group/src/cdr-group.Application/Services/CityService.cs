using AutoMapper;
using cdr_group.Contracts.DTOs.City;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class CityService : BaseService<City, CityDto, CreateCityDto, UpdateCityDto>, ICityService
    {
        public CityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<City> Repository => UnitOfWork.Cities;

        public override async Task<PagedResult<CityDto>> GetPagedAsync(PagedRequest request)
        {
            var cityRequest = request as CityPagedRequest ?? new CityPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };
            var (items, totalCount) = await UnitOfWork.Cities.GetCitiesPagedAsync(cityRequest);
            var dtos = Mapper.Map<List<CityDto>>(items);
            return new PagedResult<CityDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<PagedResult<CityDto>> GetCitiesPagedAsync(CityPagedRequest request)
        {
            var (items, totalCount) = await UnitOfWork.Cities.GetCitiesPagedAsync(request);
            var dtos = Mapper.Map<List<CityDto>>(items);
            return new PagedResult<CityDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
