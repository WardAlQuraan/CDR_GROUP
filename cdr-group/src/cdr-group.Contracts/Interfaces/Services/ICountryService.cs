using cdr_group.Contracts.DTOs.Country;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ICountryService : IBaseService<CountryDto, CreateCountryDto, UpdateCountryDto>
    {
    }
}
