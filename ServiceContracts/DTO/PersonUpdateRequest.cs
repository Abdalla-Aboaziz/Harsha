using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO;

public class PersonUpdateRequest
{
    [Required(ErrorMessage = "PersonID is required")]
    public Guid PersonID { get; set; }

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

    public Person ToPerson()
    {
        return new Person()
        {
            PersonID = PersonID,
            PersonName = PersonName,
            Email = Email,
            CountryID = CountryID,
            DateOfBirth = DateOfBirth,
            Gender = Gender?.ToString(),
            Address = Address,
            ReceiveNewsletter = ReceiveNewsletter
        };

    }
}
