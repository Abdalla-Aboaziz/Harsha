using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Linq.Expressions;



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

        //int rowsDeleted = await _dbContext.Persons
        //                                    .Where(p => p.PersonID == personId)   // Best in Production : because it will check whether the person exists or not. If the person does not exist, it will not throw an exception. It will just return 0 rows deleted.
        //                                    .ExecuteDeleteAsync();

        _dbContext.Persons.RemoveRange(_dbContext.Persons.Where(temp => temp.PersonID == personId));
        int rowsDeleted = await _dbContext.SaveChangesAsync();   // For Testing Purpose 
        return rowsDeleted > 0;

    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        return await _dbContext.Persons.Include(p => p.Country).ToListAsync();
    }

    public async Task<List<Person>> GetFilteredPersonsAsync(Expression<Func<Person, bool>> predicate)
    {
        return await _dbContext.Persons.Include(p => p.Country).Where(predicate).ToListAsync();
    }

    public async Task<Person?> GetPersonByPersonIdAsync(Guid personId)
    {
        return await _dbContext.Persons.Include(p => p.Country).FirstOrDefaultAsync(x => x.PersonID == personId);
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        var existingPerson = await _dbContext.Persons.FirstOrDefaultAsync(x => x.PersonID == person.PersonID);
        if (existingPerson == null)
        {
            throw new InvalidOperationException("Person not found");
        }

        // Update the existing person with the new values
        existingPerson.PersonName = person.PersonName;
        existingPerson.Address = person.Address;
        existingPerson.DateOfBirth = person.DateOfBirth;
        existingPerson.Gender = person.Gender;

        existingPerson.CountryID = person.CountryID; // Update the CountryID if needed
        existingPerson.ReceiveNewsLetters = person.ReceiveNewsLetters;
        existingPerson.Email = person.Email;



        await _dbContext.SaveChangesAsync();
        return existingPerson;
    }
}
