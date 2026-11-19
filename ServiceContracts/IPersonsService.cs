using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts;

public interface IPersonsService
{
    PersonResponse AddPerson(PersonAddRequest? personAddRequest);
    List<PersonResponse> GetAllPersons();
    PersonResponse? GetPersonByPersonId(Guid? personId);
    List<PersonResponse> GetFilteredPerson(string searchBy, string searchString);
    List<PersonResponse> GetSortedPerson(List<PersonResponse> allPersons, string sortby, SortOrderOptions sortOrderOptions);
    PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);
    bool DeletePerson(Guid? personId);

}
