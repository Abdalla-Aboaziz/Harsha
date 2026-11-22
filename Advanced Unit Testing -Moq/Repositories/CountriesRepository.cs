using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories;

public class CountriesRepository : ICountriesRepository
{
    private readonly Entities.ApplicationDbContext _dbContext;

    public CountriesRepository(Entities.ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }
    public async Task<Country> AddCountryAsync(Country country)
    {
        _dbContext.Countries.Add(country);
        await _dbContext.SaveChangesAsync();
        return country;
    }

    public async Task<List<Country>> GetAllCountriesAsync()
    {
        return await _dbContext.Countries.ToListAsync();
    }

    public Task<Country?> GetCountryByCountryIdAsync(Guid countryId)
    {
        return _dbContext.Countries.FirstOrDefaultAsync(x => x.CountryID == countryId);
    }

    public Task<Country?> GetCountryByCountryNameAsync(string countryName)
    {
        return _dbContext.Countries.FirstOrDefaultAsync(x => x.CountryName == countryName);
    }
}
