using System.ComponentModel.DataAnnotations;

namespace Services.Helpers;

public class ValidationHelper
{
    internal static void ModelValidation(object obj)
    {
        ValidationContext validationContext = new ValidationContext(obj);
        ICollection<ValidationResult>? validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(obj, validationContext, validationResults, true))
        {
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }

    }
}
