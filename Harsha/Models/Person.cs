using Harsha.CustomValid;
using System.ComponentModel.DataAnnotations;

namespace Harsha.Models
{
    public class Person: IValidatableObject
    {
        [Required]
        public string Name { get; set; }
   
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        [BirthDateValidator(2005,ErrorMessage ="TEST Ya Khoya Elwad Sokhier")]
        public DateTime? BirthDate { get; set; }

        public DateTime? FromDate { get; set; }
        [DateRangeValidator("FromDate", ErrorMessage = "ToDate must be greater than or equal to FromDate ")]

        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }
        public List<string?> Tags { get; set; } = new List<string?>();
        public override string ToString()
        {
            return $" Name: {Name}, Phone: {Phone}, Email: {Email}, Password: {Password}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(BirthDate.HasValue==false && Age.HasValue == false)
            {
              yield  return new ValidationResult("Either BirthDate or Age must be provided.",new[]
                {
                    nameof(BirthDate),
                    nameof(Age)
                });
            }
            
        }
    }
}
