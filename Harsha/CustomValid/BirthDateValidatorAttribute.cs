using System.ComponentModel.DataAnnotations;

namespace Harsha.CustomValid
{
    public class BirthDateValidatorAttribute: ValidationAttribute
    {
        public int MinYear { get; set; } = 2000;
        public BirthDateValidatorAttribute()
        {
            
        }
        public BirthDateValidatorAttribute(int minYear)
        {
            MinYear = minYear;
        }

       

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                if (birthDate.Year >= MinYear)
                {
                    return new ValidationResult(ErrorMessage ?? "Birth date must be before January 1, 2000.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid birth date format.");
        }
    }

    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        private readonly string _otherPropertyName;

        public DateRangeValidatorAttribute(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // current value (ToDate مثلا)
            if (value == null)
                return ValidationResult.Success;

            DateTime currentDate = (DateTime)value;

            // get other property (FromDate مثلا)
            var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherProperty == null)
            {
                return new ValidationResult($"Unknown property: {_otherPropertyName}");
            }

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            if (otherValue == null)
                return ValidationResult.Success;

            DateTime otherDate = (DateTime)otherValue;

            // validation logic
            if (currentDate < otherDate)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"{validationContext.MemberName} must be >= {_otherPropertyName}",
                    new[] { validationContext.MemberName!, _otherPropertyName }
                );
            }

            return ValidationResult.Success;
        }
    }
}
