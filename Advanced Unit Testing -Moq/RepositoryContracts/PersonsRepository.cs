using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RepositoryContracts;

public class PersonsRepository : IPersonsRepository
{
    private readonly Entities.ApplicationDbContext _dbContext;

    public PersonsRepository(Entities.ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }
    public async Task<Person> AddPersonAsync(Person person)
    {
        _dbContext.Persons.Add(person);
        await _dbContext.SaveChangesAsync();
        return person;
    }

    public async Task<bool> DeletePerson(Guid personId)
    {
        //_dbContext.Persons.Remove(new Person { PersonID = personId });    //  Bad Approach : because it will not check whether the person exists or not. It will just create a new person object with the given personId and try to delete it. If the person does not exist, it will throw an exception.
        //return await _dbContext.SaveChangesAsync() > 0;

        int rowsDeleted = await _dbContext.Persons
                                            .Where(p => p.PersonID == personId)
                                            .ExecuteDeleteAsync();
        return rowsDeleted > 0;
    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        return await _dbContext.Persons.ToListAsync();
    }

    public async Task<List<Person>> GetFilteredPersonsAsync(Expression<Func<Person, bool>> predicate)
    {
        return await _dbContext.Persons.Where(predicate).ToListAsync();
    }

    public Task<Person?> GetPersonByPersonIdAsync(Guid personId)
    {
        return _dbContext.Persons.FirstOrDefaultAsync(x => x.PersonID == personId);
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync();
        return person;
    }
}
