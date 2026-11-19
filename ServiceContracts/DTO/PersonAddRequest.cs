using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO;

public class PersonAddRequest
{
    [Required(ErrorMessage = "Person name is required")]
    public string? PersonName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }
    public Guid? CountryID { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions? Gender { get; set; }
    public string? Address { get; set; }
    public bool ReceiveNewsletter { get; set; }

}

public static class PersonAddRequestExtentions
{
    public static Person ToPerson(this PersonAddRequest personAddRequest)
    {
        return new Person()
        {
            PersonName = personAddRequest.PersonName,
            Email = personAddRequest.Email,
            CountryID = personAddRequest.CountryID,
            DateOfBirth = personAddRequest.DateOfBirth,
            Gender = personAddRequest.Gender.ToString(),
            Address = personAddRequest.Address,
            ReceiveNewsletter = personAddRequest.ReceiveNewsletter
        };
    }
}

