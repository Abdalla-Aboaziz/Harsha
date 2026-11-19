using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace ContactsManger.Tests
{
    public class PersionServiceTest
    {
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        // constructor
        public PersionServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonsService();
            _countriesService = new CountryService();
            _testOutputHelper = testOutputHelper;
        }
        #region AddPerson



        // when PersonAddRequest is null, then AddPerson should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPersonAddRequest_ThrowsArgumentNullException()
        {
            // Arrange
            PersonAddRequest? PersonAddRequest = null;
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act 
                _personService.AddPerson(PersonAddRequest);
            });


        }
        // when PersonName is null, then AddPerson should throw ArgumentException

        [Fact]
        public void AddPerson_NullPersonName_ThrowsArgumenException()
        {
            // Arrange
            PersonAddRequest? PersonAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act 
                _personService.AddPerson(PersonAddRequest);
            });
        }


        //When you supply proper Person Details , it should insert(add) the Person  to the existing list of persons
        //and return the PersonResponse object with the generated PersonId
        [Fact]
        public void AddPerson_ProperPersonDetails_InsertsPerson()
        {
            // Arrange
            PersonAddRequest? PersonAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "person@example.com",
                Address = "123 Main St",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                ReceiveNewsletter = true
            };

            //Act
            PersonResponse person_response_from_add = _personService.AddPerson(PersonAddRequest);
            var persion_list = _personService.GetAllPersons();
            // Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, persion_list);

        }
        #endregion

        #region GetAllPersons
        // the list of Persons should be empty by default(before adding any Persons)
        [Fact]
        public void GetAllPersons_EmpityList()
        {
            /// Act
            List<PersonResponse> actual_Person_response_list = _personService.GetAllPersons();

            // Assert
            Assert.Empty(actual_Person_response_list);
        }

        //[Fact]
        //public void GetAllPersons_AddFewPersons()
        //{
        //   CountryAddRequest countryAddRequest_1 = new CountryAddRequest()
        //   {
        //       CountryName = "USA"
        //   };
        //    var country_response_1 = _countriesService.AddCountry(countryAddRequest_1);
        //    CountryAddRequest countryAddRequest_2 = new CountryAddRequest()
        //    {
        //        CountryName = "Canada"
        //    };
        //    var country_response_2 = _countriesService.AddCountry(countryAddRequest_2);
        //    PersonAddRequest PersonAddRequest_1 = new PersonAddRequest()
        //    {
        //        PersonName = "John Doe",
        //        Email = "john.doe@example.com",
        //        CountryID = country_response_1.CountryId,
        //        Address = "123 Main St",
        //        Gender = GenderOptions.Male,
        //        DateOfBirth = new DateTime(1990, 1, 1),
        //        ReceiveNewsletter = true
        //    };
        //    PersonResponse addedPerson_response_1 = _personService.AddPerson(PersonAddRequest_1);

        //    PersonAddRequest PersonAddRequest_2 = new PersonAddRequest()
        //    {
        //        PersonName = "Jane Smith",
        //        Email = "jane.smith@example.com",
        //        CountryID = country_response_2.CountryId,
        //        Address = "456 Oak Ave",
        //        Gender = GenderOptions.Female,
        //        DateOfBirth = new DateTime(1985, 5, 15),
        //        ReceiveNewsletter = false
        //    };
        //    PersonResponse addedPerson_response_2 = _personService.AddPerson(PersonAddRequest_2);

        //    // Act
        //    List<PersonResponse> actual_Person_response_list = _personService.GetAllPersons();

        //    // Assert
        //    Assert.Contains(addedPerson_response_1, actual_Person_response_list);
        //    Assert.Contains(addedPerson_response_2, actual_Person_response_list);
        //}

        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                Address = "address of smith",
                CountryID = country_response_1.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsletter = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "mary@example.com",
                Gender = GenderOptions.Female,
                Address = "address of mary",
                CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("2000-02-02"),
                ReceiveNewsletter = false
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male,
                Address = "address of rahman",
                CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("1999-03-03"),
                ReceiveNewsletter = true
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }
            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine($"PersonId: {person_response.PersonID}, PersonName: {person_response.PersonName}, Email: {person_response.Email}, Country: {person_response.Country}, Address: {person_response.Address}, Gender: {person_response.Gender}, DateOfBirth: {person_response.DateOfBirth}, ReceiveNewsletter: {person_response.ReceiveNewsletter}");
            }
            //Act
            List<PersonResponse> persons_list_from_get = _personService.GetAllPersons();
            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person_response in persons_list_from_get)
            {
                _testOutputHelper.WriteLine($"PersonId: {person_response.PersonID}, PersonName: {person_response.PersonName}, Email: {person_response.Email}, Country: {person_response.Country}, Address: {person_response.Address}, Gender: {person_response.Gender}, DateOfBirth: {person_response.DateOfBirth}, ReceiveNewsletter: {person_response.ReceiveNewsletter}");
            }
            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);
            }
        }

        #endregion

        #region GetPersonByPersonID

        // if we supply null as PersonId, then GetPersonByPersonId should return null
        [Fact]
        public void GetPersonByPersonID_NullPersonId()
        {
            //Arrange
            Guid? PersonId = null;
            //Act
            var Person_response_from_GetPersonByPersonId = _personService.GetPersonByPersonId(PersonId);
            // Assert
            Assert.Null(Person_response_from_GetPersonByPersonId);

        }

        // if ew supply the valid PersonId, then GetPersonByPersonId should return the  PersonResponse object
        [Fact]
        public void GetPersonByPersonId_ValidPersonId()
        {
            // Arrange 
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            var country_response = _countriesService.AddCountry(countryAddRequest);
            PersonAddRequest PersonAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "john.doe@example.com",
                CountryID = country_response.CountryId,
                Address = "123 Main St",
                Gender = GenderOptions.Male,
                DateOfBirth = new DateTime(1990, 1, 1),
                ReceiveNewsletter = true
            };
            PersonResponse addedPerson_response = _personService.AddPerson(PersonAddRequest);

            // Act
            var Person_response_from_GetPersonByPersonId = _personService.GetPersonByPersonId(addedPerson_response.PersonID);

            // Assert
            Assert.Equal(addedPerson_response, Person_response_from_GetPersonByPersonId);
        }


        #endregion

        #region GetFilteredPersons

        [Fact]
        // If the search text is empity and search by "Person Name" return all Persons
        // 
        public void GetFilteredPersons_EmptySearchText()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                Address = "address of smith",
                CountryID = country_response_1.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsletter = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "mary@example.com",
                Gender = GenderOptions.Female,
                Address = "address of mary",
                CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("2000-02-02"),
                ReceiveNewsletter = false
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male,
                Address = "address of rahman",
                CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("1999-03-03"),
                ReceiveNewsletter = true
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }
            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine($"PersonId: {person_response.PersonID}, PersonName: {person_response.PersonName}, Email: {person_response.Email}, Country: {person_response.Country}, Address: {person_response.Address}, Gender: {person_response.Gender}, DateOfBirth: {person_response.DateOfBirth}, ReceiveNewsletter: {person_response.ReceiveNewsletter}");
            }
            //Act
            List<PersonResponse> persons_list_from_search = _personService.GetFilteredPerson(nameof(Person.PersonName), "");
            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person_response in persons_list_from_search)
            {
                _testOutputHelper.WriteLine($"PersonId: {person_response.PersonID}, PersonName: {person_response.PersonName}, Email: {person_response.Email}, Country: {person_response.Country}, Address: {person_response.Address}, Gender: {person_response.Gender}, DateOfBirth: {person_response.DateOfBirth}, ReceiveNewsletter: {person_response.ReceiveNewsletter}");
            }
            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_search);
            }
        }

        [Fact]
        // First we add few persons ; and then we will search based on person name with some search string
        //  it should return the matching string 
        public void GetFilteredPersons_Search_By_PersonName()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                Address = "address of smith",
                CountryID = country_response_1.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsletter = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "mary@example.com",
                Gender = GenderOptions.Female,
                Address = "address of mary",
                CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("2000-02-02"),
                ReceiveNewsletter = false
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male,
                Address = "address of rahman",
                CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("1999-03-03"),
                ReceiveNewsletter = true
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }
            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine($"PersonId: {person_response.PersonID}, PersonName: {person_response.PersonName}, Email: {person_response.Email}, Country: {person_response.Country}, Address: {person_response.Address}, Gender: {person_response.Gender}, DateOfBirth: {person_response.DateOfBirth}, ReceiveNewsletter: {person_response.ReceiveNewsletter}");
            }
            //Act
            List<PersonResponse> persons_list_from_search = _personService.GetFilteredPerson(nameof(Person.PersonName), "ma");
            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person_response in persons_list_from_search)
            {
                _testOutputHelper.WriteLine($"PersonId: {person_response.PersonID}, PersonName: {person_response.PersonName}, Email: {person_response.Email}, Country: {person_response.Country}, Address: {person_response.Address}, Gender: {person_response.Gender}, DateOfBirth: {person_response.DateOfBirth}, ReceiveNewsletter: {person_response.ReceiveNewsletter}");
            }
            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                if (person_response_from_add.PersonName != null)
                {
                    if (person_response_from_add.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                        Assert.Contains(person_response_from_add, persons_list_from_search);
                }
            }

        }
        #endregion

        #region GetSortedPersons

        [Fact]
        //when we sorted based in PersonName in Descending , it should return persons list in descending on PersonName 
        public void GetSortedPersons()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                Address = "address of smith",
                CountryID = country_response_1.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsletter = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "mary@example.com",
                Gender = GenderOptions.Female,
                Address = "address of mary",
                CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("2000-02-02"),
                ReceiveNewsletter = false
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male,
                Address = "address of rahman",
                CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("1999-03-03"),
                ReceiveNewsletter = true
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }
            // print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine($"PersonId: {person_response.PersonID}, PersonName: {person_response.PersonName}, Email: {person_response.Email}, Country: {person_response.Country}, Address: {person_response.Address}, Gender: {person_response.Gender}, DateOfBirth: {person_response.DateOfBirth}, ReceiveNewsletter: {person_response.ReceiveNewsletter}");
            }
            var allpersons = _personService.GetAllPersons();
            //Act
            List<PersonResponse> persons_list_from_sort = _personService.GetSortedPerson(allpersons, nameof(Person.PersonName), SortOrderOptions.Descending);
            // print persons_list_from_sort
            _testOutputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person_response in persons_list_from_sort)
            {
                _testOutputHelper.WriteLine($"PersonId: {person_response.PersonID}, PersonName: {person_response.PersonName}, Email: {person_response.Email}, Country: {person_response.Country}, Address: {person_response.Address}, Gender: {person_response.Gender}, DateOfBirth: {person_response.DateOfBirth}, ReceiveNewsletter: {person_response.ReceiveNewsletter}");
            }
            person_response_list_from_add = person_response_list_from_add.OrderByDescending(temp => temp.PersonName).ToList();
            //Assert
            for (int i = 0; i < person_response_list_from_add.Count; i++)
            {
                Assert.Equal(person_response_list_from_add[i], persons_list_from_sort[i]);
            }

        }
        #endregion

        #region UpdatePerson
        // when we supply null as PersonUpdateRequest, then UpdatePerson should throw ArgumentNullException
        [Fact]
        public void UpdatePerson_NullPersonUpdateRequest_ThrowsArgumentNullException()
        {
            // Arrange
            PersonUpdateRequest? PersonUpdateRequest = null;
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act 
                _personService.UpdatePerson(PersonUpdateRequest);
            });
        }
        // when we supply invalid PersonId in PersonUpdateRequest, then UpdatePerson should throw ArgumentException
        [Fact]
        public void UpdatePerson_InvalidPersonId_ThrowsArgumentException()
        {
            // Arrange
            PersonUpdateRequest? PersonUpdateRequest = new PersonUpdateRequest()
            {
                PersonID = Guid.NewGuid(), // Non-existing PersonID
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male
            };
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act 
                _personService.UpdatePerson(PersonUpdateRequest);
            });
        }
        // when personName is null it should throw ArgumentException
        [Fact]
        public void UpdatePerson_NullPersonName_ThrowsArgumentException()
        {
            // Arrange
            CountryAddRequest countryAdd = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            var country_response_from_add = _countriesService.AddCountry(countryAdd);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                Address = "address of smith",
                CountryID = country_response_from_add.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsletter = true
            };
            var person_response_from_add = _personService.AddPerson(person_request_1);

            var person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _personService.UpdatePerson(person_update_request);
            });

        }



        // First , add new person and try  to update the person name and email
        [Fact]
        public void UpdatePerson_PersonFullDetails()
        {
            // Arrange
            CountryAddRequest countryAdd = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            var country_response_from_add = _countriesService.AddCountry(countryAdd);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                Address = "address of smith",
                CountryID = country_response_from_add.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsletter = true
            };
            var person_response_from_add = _personService.AddPerson(person_request_1);

            var person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "Abdalla";
            person_update_request.Email = "abdallaaboaziz@gmail.com";
            // Act
            var person_response_from_update = _personService.UpdatePerson(person_update_request);

            var person_response_from_getbyid = _personService.GetPersonByPersonId(person_response_from_update.PersonID);
            // Assert
            Assert.Equal(person_response_from_getbyid, person_response_from_update);
        }



        #endregion

        #region DeletePerson
        // if you supply an valid PersonId, then DeletePerson should return true
        [Fact]
        public void DeletePerson_validPersonId()
        {
            // Arrange
            CountryAddRequest countryAdd = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            var country_response_from_add = _countriesService.AddCountry(countryAdd);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                Address = "address of smith",
                CountryID = country_response_from_add.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsletter = true
            };
            var person_response_from_add = _personService.AddPerson(person_request_1);

            // Act 
            bool isDeleted = _personService.DeletePerson(person_response_from_add.PersonID);

            // Assert
            Assert.True(isDeleted);
        }
        // if you supply an invalid PersonId, then DeletePerson should return false
        [Fact]
        public void DeletePerson_InvalidPersonId()
        {
            // Arrange


            // Act 
            bool isDeleted = _personService.DeletePerson(Guid.NewGuid());

            // Assert
            Assert.False(isDeleted);
        }

        #endregion
    }
}
