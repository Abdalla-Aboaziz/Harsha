using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services;

public class PersonsService : IPersonsService
{

    private readonly List<Person> _persons;
    private readonly ICountriesService _countriesService;
    public PersonsService()
    {
        _persons = new List<Person>();
        _countriesService = new CountryService();
    }

    private PersonResponse ConvertPersonToPersonResponse(Person person)
    {
        PersonResponse personResponse = person.ToPersonResponse();

        personResponse.Country = _countriesService.GetCountryByCountryId(person.CountryID)?.CountryName;
        return personResponse;
    }

    public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
    {
        ArgumentNullException.ThrowIfNull(personAddRequest);

        // call Model Validation 
        ValidationHelper.ModelValidation(personAddRequest);

        // Convert object from PersonAddRequest to Person type
        Person person = personAddRequest.ToPerson();

        //  generate PersonId 
        person.PersonID = Guid.NewGuid();

        _persons.Add(person);

        // convert the person object to personResponse type 
        return ConvertPersonToPersonResponse(person);

    }

    public List<PersonResponse> GetAllPersons()
    {
        return [.. _persons.Select(person => person.ToPersonResponse())];
    }
    //public List<PersonResponse> GetFilteredPerson(string searchBy, string searchString)
    //{
    //    List<PersonResponse> allPersons = GetAllPersons();
    //    List<PersonResponse> matchingPersons = allPersons;

    //    if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
    //        return matchingPersons;
    //    switch (searchBy.ToLower())
    //    {
    //        case nameof(Person.PersonName):
    //            matchingPersons = allPersons.Where(temp => temp.PersonName != null && temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
    //            break;
    //        case nameof(Person.Email):
    //            matchingPersons = allPersons.Where(temp => temp.Email != null && temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
    //            break;

    //        case nameof(Person.DateOfBirth):
    //            matchingPersons = allPersons.Where(temp => temp.DateOfBirth != null && temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
    //            break;
    //        case nameof(Person.Gender):
    //            matchingPersons = allPersons.Where(temp => temp.Gender != null && temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
    //            break;
    //        case nameof(Person.Address):
    //            matchingPersons = allPersons.Where(temp => temp.Address != null && temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
    //            break;
    //        case nameof(Person.CountryID):
    //            matchingPersons = allPersons.Where(temp => temp.CountryID != null && temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
    //            break;

    //        default:
    //            matchingPersons = allPersons;
    //            break;

    //    }
    //    return matchingPersons;
    //}

    public List<PersonResponse> GetFilteredPerson(string searchBy, string searchString)
    {
        List<PersonResponse> allPersons = GetAllPersons();

        if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(searchString))
            return allPersons;

        IEnumerable<PersonResponse> query = allPersons;

        if (searchBy.Equals(nameof(Person.PersonName), StringComparison.OrdinalIgnoreCase))
            query = allPersons.Where(p => p.PersonName?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true);

        else if (searchBy.Equals(nameof(Person.Email), StringComparison.OrdinalIgnoreCase))
            query = allPersons.Where(p => p.Email?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true);

        else if (searchBy.Equals(nameof(Person.DateOfBirth), StringComparison.OrdinalIgnoreCase))
        {
            if (DateTime.TryParse(searchString, out DateTime parsedDate))
                query = allPersons.Where(p => p.DateOfBirth?.Date == parsedDate.Date);
            else
                return new List<PersonResponse>();
        }

        else if (searchBy.Equals(nameof(Person.Gender), StringComparison.OrdinalIgnoreCase))
            query = allPersons.Where(p => p.Gender?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true);

        else if (searchBy.Equals(nameof(Person.Address), StringComparison.OrdinalIgnoreCase))
            query = allPersons.Where(p => p.Address?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true);

        else if (searchBy.Equals(nameof(Person.CountryID), StringComparison.OrdinalIgnoreCase))
            query = allPersons.Where(p => p.Country?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true);

        return query.ToList();
    }


    public PersonResponse? GetPersonByPersonId(Guid? personId)
    {
        if (personId == Guid.Empty)
            return null;

        Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personId);
        if (person == null)
            return null;
        //return ConvertPersonToPersonResponse(person);
        return person.ToPersonResponse();
    }

    public List<PersonResponse> GetSortedPerson(List<PersonResponse> allPersons, string sortby, SortOrderOptions sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortby))
            return allPersons;
        List<PersonResponse> sortedPersons = (sortby, sortOrder) switch
        {
            (nameof(PersonResponse.PersonName), var order) when order == SortOrderOptions.Ascending => allPersons.OrderBy(p => p.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.PersonName), var order) when order == SortOrderOptions.Descending => allPersons.OrderByDescending(p => p.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

            (nameof(PersonResponse.Email), var order) when order == SortOrderOptions.Ascending => allPersons.OrderBy(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Email), var order) when order == SortOrderOptions.Descending => allPersons.OrderByDescending(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

            (nameof(PersonResponse.DateOfBirth), var order) when order == SortOrderOptions.Ascending => allPersons.OrderBy(p => p.DateOfBirth).ToList(),
            (nameof(PersonResponse.DateOfBirth), var order) when order == SortOrderOptions.Descending => allPersons.OrderByDescending(p => p.DateOfBirth).ToList(),

            (nameof(PersonResponse.Age), var order) when order == SortOrderOptions.Ascending => allPersons.OrderBy(p => p.Age).ToList(),
            (nameof(PersonResponse.Age), var order) when order == SortOrderOptions.Descending => allPersons.OrderByDescending(p => p.Age).ToList(),

            (nameof(PersonResponse.Gender), var order) when order == SortOrderOptions.Ascending => allPersons.OrderBy(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Gender), var order) when order == SortOrderOptions.Descending => allPersons.OrderByDescending(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

            (nameof(PersonResponse.Country), var order) when order == SortOrderOptions.Ascending => allPersons.OrderBy(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Country), var order) when order == SortOrderOptions.Descending => allPersons.OrderByDescending(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

            (nameof(PersonResponse.Address), var order) when order == SortOrderOptions.Ascending => allPersons.OrderBy(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Address), var order) when order == SortOrderOptions.Descending => allPersons.OrderByDescending(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),

            (nameof(PersonResponse.ReceiveNewsletter), var order) when order == SortOrderOptions.Ascending => allPersons.OrderBy(p => p.ReceiveNewsletter).ToList(),
            (nameof(PersonResponse.ReceiveNewsletter), var order) when order == SortOrderOptions.Descending => allPersons.OrderByDescending(p => p.ReceiveNewsletter).ToList(),

            _ => allPersons
        };
        return sortedPersons;
    }

    public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
    {
        ArgumentNullException.ThrowIfNull(personUpdateRequest);
        ValidationHelper.ModelValidation(personUpdateRequest);
        var existingPerson = _persons
                                    .FirstOrDefault(p => p.PersonID == personUpdateRequest.PersonID)
                                    ?? throw new ArgumentException($"Person with ID {personUpdateRequest.PersonID} not found.");
        // Update the properties of the existing person
        existingPerson.PersonName = personUpdateRequest.PersonName;
        existingPerson.Email = personUpdateRequest.Email;
        existingPerson.CountryID = personUpdateRequest.CountryID;
        existingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
        existingPerson.Gender = personUpdateRequest.Gender.ToString();
        existingPerson.Address = personUpdateRequest.Address;
        existingPerson.ReceiveNewsletter = personUpdateRequest.ReceiveNewsletter;
        return ConvertPersonToPersonResponse(existingPerson);
    }

    public bool DeletePerson(Guid? personId)
    {
        if (personId == null)
            throw new ArgumentNullException(nameof(personId));

        var person = _persons.FirstOrDefault(p => p.PersonID == personId);
        if (person == null)
            return false;

        _persons.Remove(person);
        return true;
    }
}

