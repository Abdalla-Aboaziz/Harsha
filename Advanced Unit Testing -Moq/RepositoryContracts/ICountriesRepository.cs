using Entities;

namespace RepositoryContracts;

public interface ICountriesRepository
{
    Task<Country> AddCountryAsync(Country country);
    Task<List<Country>> GetAllCountriesAsync();
    Task<Country?> GetCountryByCountryIdAsync(Guid countryId);
    Task<Country?> GetCountryByCountryNameAsync(string countryName);

}
