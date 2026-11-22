using AutoFixture;
using CRUDExample.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CRUDTests;

public class PersonsControllerTests
{
    private readonly IPersonsService _personsService;
    private readonly ICountriesService _countriesService;

    private readonly Mock<IPersonsService> _personsServiceMock;
    private readonly Mock<ICountriesService> _countriesServiceMock;
    private readonly Fixture _fixture;
    public PersonsControllerTests()
    {
        _fixture = new Fixture();
        _personsServiceMock = new Mock<IPersonsService>();
        _countriesServiceMock = new Mock<ICountriesService>();
        _personsService = _personsServiceMock.Object;
        _countriesService = _countriesServiceMock.Object;
    }


    #region Index 

    [Fact]
    public async Task Index_ShouldReturnIndexViewWithPersonsList()
    {
        // Arrange
        List<PersonResponse> persons_response_list = _fixture.Create<List<PersonResponse>>();
        PersonsController personsController = new PersonsController(_personsService, _countriesService);

        _personsServiceMock
            .Setup(t => t.GetFilteredPersons(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(persons_response_list);

        _personsServiceMock
            .Setup(t => t.GetSortedPersons(It.IsAny<List<PersonResponse>>(), It.IsAny<string>(), It.IsAny<SortOrderOptions>()))
            .ReturnsAsync(persons_response_list);
        // Act
        var result = await personsController.Index(_fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<SortOrderOptions>());
        // Assert
        ViewResult viewResult = Assert.IsType<ViewResult>(result);
        viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<PersonResponse>>();
        viewResult.ViewData.Model.Should().Be(persons_response_list);
    }

    #endregion

    #region Create

    [Fact]
    public async void Create_IfModelErrors_ToReturnCreateView()
    {
        // Arrange
        PersonAddRequest persons_add_request =
            _fixture.Create<PersonAddRequest>();

        PersonResponse personResponse =
            _fixture.Create<PersonResponse>();

        List<CountryResponse> countries_response_list =
            _fixture.Create<List<CountryResponse>>();

        _countriesServiceMock.Setup(t => t.GetAllCountries())
            .ReturnsAsync(countries_response_list);

        _personsServiceMock.Setup(t => t.AddPerson(It.IsAny<PersonAddRequest>()))
            .ReturnsAsync(personResponse);

        PersonsController personsController = new PersonsController(_personsService, _countriesService);


        // Act
        personsController.ModelState.AddModelError("PersonName", "PersonName can not be blank");
        var result = await personsController.Create(persons_add_request);

        // Assert
        ViewResult viewResult = Assert.IsType<ViewResult>(result);
        viewResult.ViewData.Model.Should().BeAssignableTo<PersonAddRequest>();
        viewResult.ViewData.Model.Should().Be(persons_add_request);
    }


    [Fact]
    public async Task Create_IfNoModelErrors_ToReturnRedirectToIndex()
    {
        // Arrange
        PersonAddRequest persons_add_request =
            _fixture.Create<PersonAddRequest>();

        PersonResponse personResponse =
            _fixture.Create<PersonResponse>();

        List<CountryResponse> countries_response_list =
            _fixture.Create<List<CountryResponse>>();

        _countriesServiceMock.Setup(t => t.GetAllCountries())
            .ReturnsAsync(countries_response_list);

        _personsServiceMock.Setup(t => t.AddPerson(It.IsAny<PersonAddRequest>()))
            .ReturnsAsync(personResponse);

        PersonsController personsController = new PersonsController(_personsService, _countriesService);


        // Act

        var result = await personsController.Create(persons_add_request);

        // Assert
        RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        redirectToActionResult.ActionName.Should().Be("Index");
    }

    #endregion

}
