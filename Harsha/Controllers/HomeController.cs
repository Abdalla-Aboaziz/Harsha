using Harsha.CustomModelBinding;
using Harsha.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Harsha.Controllers
{
    public class HomeController : Controller
    {



        // [ModelBinder(BinderType = typeof(PersonModelBindeder))] 

        public async Task<IActionResult> Index(Person person)
    
        {
          // await HttpContext.Response.WriteAsJsonAsync(person);
            if (!ModelState.IsValid)
            {
                 string errorMessages = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(errorMessages);
            }
            return Content($"{person}");
        }
    }
}
