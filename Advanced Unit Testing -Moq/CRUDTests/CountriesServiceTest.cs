using AutoFixture;
using Entities;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
        private readonly ICountriesRepository _countriesRepository;
        private readonly Mock<ICountriesRepository> _countriesRepositoryMock;
        private readonly IFixture _fixture;

        //constructor
        public CountriesServiceTest()
        {
            _fixture = new Fixture();

            _countriesRepositoryMock = new Mock<ICountriesRepository>();
            _countriesRepository = _countriesRepositoryMock.Object;

            #region DbContext Mocking
            //var countriesInitialData = new List<Country>() { };

            //DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
            //  new DbContextOptionsBuilder<ApplicationDbContext>().Options
            // );

            //ApplicationDbContext dbContext = dbContextMock.Object;
            //dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);

            //_countriesService = new CountriesService(dbContext);  before reop
            #endregion

            _countriesService = new CountriesService(_countriesRepository);
        }


        #region AddCountry

        //When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Act
            var action = async () =>
            {
                await _countriesService.AddCountry(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }


        //When the CountryName is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_CountryNameIsNull()
        {
            // Arrange
            CountryAddRequest request = _fixture.Build<CountryAddRequest>()
                .With(temp => temp.CountryName, null as string)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _countriesService.AddCountry(request);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();

            _countriesRepositoryMock.Verify(
                temp => temp.GetCountryByCountryNameAsync(It.IsAny<string>()),
                Times.Never
            );

            _countriesRepositoryMock.Verify(
                temp => temp.AddCountryAsync(It.IsAny<Country>()),
                Times.Never
            );
        }

        //When the CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_DuplicateCountryName_ToBeArgumentException()
        {
            Country country = _fixture.Build<Country>()
             .With(temp => temp.CountryName, "India")
             .With(t => t.Persons, new List<Person>())
             .Create();

            CountryAddRequest request = _fixture.Build<CountryAddRequest>()
              .With(temp => temp.CountryName, "India")
              .Create();
            _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryNameAsync("India"))
                .ReturnsAsync(country);


            //Act
            var action = async () =>
            {
                await _countriesService.AddCountry(request);

            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
            _countriesRepositoryMock.Verify(
                                            temp => temp.AddCountryAsync(It.IsAny<Country>()),
                                            Times.Never
                                        );
        }


        //When you supply proper country name, it should insert (add) the country to the existing list of countries
        [Fact]
        public async Task AddCountry_ProperCountryDetails_ToBeSuccessful()
        {
            //Arrange
            CountryAddRequest? request = _fixture.Build<CountryAddRequest>()
             .Create();
            Country country = request.ToCountry();
            CountryResponse countryResponse = country.ToCountryResponse();
            _countriesRepositoryMock.Setup(temp => temp.AddCountryAsync(It.IsAny<Country>()))
                                .ReturnsAsync(country);
            _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryNameAsync(It.IsAny<string>()))
                                .ReturnsAsync((Country)null);

            //Act
            CountryResponse country_response_from_add = await _countriesService.AddCountry(request);
            country.CountryID = country_response_from_add.CountryID;
            countryResponse.CountryID = country_response_from_add.CountryID;



            //Assert
            country_response_from_add.CountryID.Should().NotBe(Guid.Empty);
            country_response_from_add.Should().BeEquivalentTo(countryResponse);
        }

        #endregion


        #region GetAllCountries

        [Fact]
        //The list of countries should be empty by default (before adding any countries)
        public async Task GetAllCountries_EmptyList()
        {
            // Arrange 
            List<Country> countries_empty_list = new List<Country>();
            _countriesRepositoryMock.Setup(temp => temp.GetAllCountriesAsync())
                                .ReturnsAsync(countries_empty_list);

            //Act
            List<CountryResponse> actual_country_response_list = await _countriesService.GetAllCountries();

            //Assert
            actual_country_response_list.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<Country> country_list = new List<Country>()
            {
                  _fixture.Build<Country>()
                  .With(t=>t.Persons , null as List<Person>)
                   .Create(),
                  _fixture.Build<Country>()
                  .With(t=>t.Persons , null as List<Person>)
                   .Create()
            };
            List<CountryResponse> country_response_list = country_list.ConvertAll(temp => temp.ToCountryResponse());
            _countriesRepositoryMock.Setup(temp => temp.GetAllCountriesAsync())
                                .ReturnsAsync(country_list);
            //Act

            List<CountryResponse> actualCountryResponseList = await _countriesService.GetAllCountries();

            //Assert
            actualCountryResponseList.Should().BeEquivalentTo(country_response_list);
        }
        #endregion


        #region GetCountryByCountryID

        [Fact]
        //If we supply null as CountryID, it should return null as CountryResponse
        public async Task GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countrID = null;
            _countriesRepositoryMock
              .Setup(temp => temp.GetCountryByCountryIdAsync(It.IsAny<Guid>()))
              .ReturnsAsync(null as Country);
            //Act
            CountryResponse? country_response_from_get_method = await _countriesService.GetCountryByCountryID(countrID);


            //Assert
            country_response_from_get_method.Should().BeNull();
        }


        [Fact]
        //If we supply a valid country id, it should return the matching country details as CountryResponse object
        public async Task GetCountryByCountryID_ValidCountryID()
        {
            //Arrange
            Country? country = _fixture.Build<Country>()
                .With(t => t.Persons, null as List<Person>)
             .Create();
            CountryResponse countryResponse = country.ToCountryResponse();

            _countriesRepositoryMock
              .Setup(temp => temp.GetCountryByCountryIdAsync(It.IsAny<Guid>()))
              .ReturnsAsync(country);



            //Act
            CountryResponse? country_response_from_get = await _countriesService.GetCountryByCountryID(country.CountryID);

            //Assert
            country_response_from_get.Should().Be(countryResponse);
        }
        #endregion
    }
}
