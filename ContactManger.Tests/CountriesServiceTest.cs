using ServiceContracts;

namespace ContactsManger.Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }


    }
}
