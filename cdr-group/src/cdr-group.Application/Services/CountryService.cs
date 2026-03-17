using AutoMapper;
using cdr_group.Contracts.DTOs.Country;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class CountryService : BaseService<Country, CountryDto, CreateCountryDto, UpdateCountryDto>, ICountryService
    {
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Country> Repository => UnitOfWork.Countries;

        public async Task<IEnumerable<CountryDto>> GetCountriesWithCitiesAsync()
        {
            var countries = await UnitOfWork.Countries.GetCountriesWithCitiesAsync();
            return Mapper.Map<IEnumerable<CountryDto>>(countries);
        }
    }
}
