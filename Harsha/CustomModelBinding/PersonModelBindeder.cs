using Harsha.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Harsha.CustomModelBinding
{
    public class PersonModelBindeder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Person person = new Person();
            // Get First Name and LastName 
            if (bindingContext.ValueProvider.GetValue("FirstName").Count() > 0)
            {
                person.Name = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;

                if (bindingContext.ValueProvider.GetValue("LastName").Count() > 0)
                {
                    person.Name += " " + bindingContext.ValueProvider.GetValue("LastName").FirstValue;
                }
            }
            // EMail
            if (bindingContext.ValueProvider.GetValue("Email").Count() > 0)
            {
                person.Email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
            }
            // Phone
            if (bindingContext.ValueProvider.GetValue("Phone").Count() > 0)
            {
                person.Phone = bindingContext.ValueProvider.GetValue("Phone").FirstValue;
            }
            // Password
            if (bindingContext.ValueProvider.GetValue("Password").Count() > 0)
            {
                person.Password = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Password").FirstValue);
            }
            // BirthDate
            if (bindingContext.ValueProvider.GetValue("BirthDate").Count() > 0)
            {
                person.BirthDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("BirthDate").FirstValue);
            }
            // FromDate
            if (bindingContext.ValueProvider.GetValue("FromDate").Count() > 0)
            {
                person.FromDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("FromDate").FirstValue);
            }
            // ToDate
            if (bindingContext.ValueProvider.GetValue("ToDate").Count() > 0)
            {
                person.ToDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("ToDate").FirstValue);
            }
            // Age
            if (bindingContext.ValueProvider.GetValue("Age").Count() > 0)
            {
                person.Age = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Age").FirstValue);
            }
            bindingContext.Result = ModelBindingResult.Success(person);
            return Task.CompletedTask;
        }
    }
}
