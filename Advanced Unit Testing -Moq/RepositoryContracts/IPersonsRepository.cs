using Entities;
using System.Linq.Expressions;

namespace RepositoryContracts;

public interface IPersonsRepository
{
    Task<Person> AddPersonAsync(Person person);
    Task<List<Person>> GetAllPersonsAsync();
    Task<Person?> GetPersonByPersonIdAsync(Guid personId);
    Task<List<Person>> GetFilteredPersonsAsync(Expression<Func<Person, bool>> predicate);
    Task<bool> DeletePerson(Guid personId);
    Task<Person> UpdatePerson(Person person);


}
