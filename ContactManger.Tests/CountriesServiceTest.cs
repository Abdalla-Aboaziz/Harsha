using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace ContactsManger.Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        // constructor
        public CountriesServiceTest()
        {
            _countriesService = new CountryService();
        }
        #region AddCountry



        // when CountryAddRequest is null, then AddCountry should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountryAddRequest_ThrowsArgumentNullException()
        {
            // Arrange
            CountryAddRequest? countryAddRequest = null;
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act 
                _countriesService.AddCountry(countryAddRequest);
            });


        }
        // when CountryName is null, then AddCountry should throw ArgumentException

        [Fact]
        public void AddCountry_NullCountryName_ThrowsArgumenException()
        {
            // Arrange
            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = null
            };
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act 
                _countriesService.AddCountry(countryAddRequest);
            });
        }
        // when CountryName is Dublicate, then AddCountry should throw ArgumentException
        [Fact]
        public void AddCountry_DublicateCountryName_ThrowsArgumentException()
        { // Arrange
            CountryAddRequest request1 = new()
            {
                CountryName = "USA"
            };

            CountryAddRequest request2 = new()
            {
                CountryName = "USA"
            };

            // Act
            _countriesService.AddCountry(request1);

            // Assert
            Assert.Throws<ArgumentException>(() =>
                _countriesService.AddCountry(request2));
        }

        //When you supply proper Country Name , it should insert(add) the country  to the existing list of countries 
        [Fact]
        public void AddCountry_ProperCountryDetails_InsertsCountry()
        {
            // Arrange
            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Japan"
            };

            //Act
            CountryResponse response = _countriesService.AddCountry(countryAddRequest);
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries();
            // Assert
            Assert.True(response.CountryId != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
        }
        #endregion

        #region GetAllCountries
        // the list of countries should be empty by default(before adding any countries)
        [Fact]
        public void GetAllCountries_EmpityList()
        {
            // Act
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();

            // Assert
            Assert.Empty(actual_country_response_list);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            // Arrange
            List<CountryAddRequest> country_request_list = new()
            {
                new CountryAddRequest()
                {
                    CountryName = "USA"
                },
                new CountryAddRequest()
                {
                    CountryName = "India"
                }
            };

            // Act
            List<CountryResponse> Countries_list_from_add_country = new List<CountryResponse>();

            foreach (var country in country_request_list)
            {
                Countries_list_from_add_country.Add(_countriesService.AddCountry(country));
            }

            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();

            // read each element from countries_list_from_add_country and check whether it is present in actual_country_response_list or not
            foreach (var exepected_country in Countries_list_from_add_country)
            {
                // Assert.Contains(actual_country_response_list, temp => temp.CountryId == exepected_country.CountryId && temp.CountryName == exepected_country.CountryName);

                Assert.Contains(exepected_country, actual_country_response_list);

            }

        }

        #endregion

        #region GetCountryByCountryID

        // if we supply null as countryId, then GetCountryByCountryId should return null
        [Fact]
        public void GetCountryByCountryID_NullCountryId()
        {
            //Arrange
            Guid? CountryId = null;
            //Act
            var country_response_from_GetCountryByCountryId = _countriesService.GetCountryByCountryId(CountryId);
            // Assert
            Assert.Null(country_response_from_GetCountryByCountryId);

        }

        // if ew supply the valid countryId, then GetCountryByCountryId should return the  CountryResponse object
        [Fact]
        public void GetCountryByCountrtyId_ValidCountryId()
        {
            // Arrange 
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryResponse addedCountry_response = _countriesService.AddCountry(countryAddRequest);

            // Act
            var country_response_from_GetCountryByCountryId = _countriesService.GetCountryByCountryId(addedCountry_response.CountryId);

            // Assert
            Assert.Equal(addedCountry_response, country_response_from_GetCountryByCountryId);
        }


        #endregion
    }
}
