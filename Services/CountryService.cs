using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountryService : ICountriesService
    {
        // private field
        private readonly List<Country> _countries;
        public CountryService()
        {
            _countries = new List<Country>();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // valiadation : CountryAddRequest should not be null
            ArgumentNullException.ThrowIfNull(countryAddRequest);
            // validation : CountryName should not be null or empty
            if (string.IsNullOrEmpty(countryAddRequest.CountryName))
            {
                throw new ArgumentException("Country name cannot be null or empty", nameof(countryAddRequest.CountryName));
            }
            // validation : CountryName should not be duplicate
            if (_countries.Any(temp => temp.CountryName.Equals(countryAddRequest.CountryName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Country name cannot be duplicate", nameof(countryAddRequest.CountryName));
            }

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            //  generate CountryId 
            country.CountryId = Guid.NewGuid();

            //Add country object  into _countries list
            _countries.Add(country);

            return country.ToCountryResponse();
        }


        public List<CountryResponse> GetAllCountries()
        {
            return [.. _countries.Select(country => country.ToCountryResponse())];
        }

        public CountryResponse? GetCountryByCountryId(Guid? countryId)
        {
            if (countryId == Guid.Empty)
                return null;

            Country? country = _countries.FirstOrDefault(temp => temp.CountryId == countryId);
            if (country == null)
                return null;
            return country.ToCountryResponse();
        }
    }
}
