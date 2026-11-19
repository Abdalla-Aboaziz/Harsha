using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

public class PersonResponse
{

    public Guid PersonID { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public Guid? CountryID { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public bool ReceiveNewsletter { get; set; }
    public string? Age { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (!(obj is PersonResponse))
            return false;

        var person = (PersonResponse)obj;

        return PersonID == person.PersonID
            && PersonName == person.PersonName
            && Email == person.Email
            && CountryID == person.CountryID
            && DateOfBirth == person.DateOfBirth
            && Gender == person.Gender
            && Address == person.Address
            && ReceiveNewsletter == person.ReceiveNewsletter;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PersonID, PersonName, Email, CountryID, DateOfBirth, Gender, Address, ReceiveNewsletter);
    }

    public PersonUpdateRequest ToPersonUpdateRequest()
    {
        return new PersonUpdateRequest()
        {
            PersonID = PersonID,
            PersonName = PersonName,
            Email = Email,
            CountryID = CountryID,
            DateOfBirth = DateOfBirth,
            Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
            Address = Address,
            ReceiveNewsletter = ReceiveNewsletter,

        };
    }



}

public static class PersonResponseExtentions
{
    public static PersonResponse ToPersonResponse(this Person person)
    {
        return new PersonResponse()
        {
            PersonID = person.PersonID,
            PersonName = person.PersonName,
            Email = person.Email,
            CountryID = person.CountryID,
            DateOfBirth = person.DateOfBirth,
            Gender = person.Gender,
            Address = person.Address,
            ReceiveNewsletter = person.ReceiveNewsletter,
            Age = person.DateOfBirth.HasValue
            ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25).ToString()
            : null
        };
    }



}
