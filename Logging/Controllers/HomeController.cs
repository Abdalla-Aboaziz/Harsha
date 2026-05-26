
using Logging_And_Exceptions_Handling.Exception_Handling;
using Logging_And_Exceptions_Handling.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;

namespace Logging.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Products> _ProductsList =
                 [
              new Products{Id=1,Name="Laptop",Price=2000},
              new Products{Id=2,Name="Mobile",Price=1000},
              new Products{Id=3,Name="Tablet",Price=1500}
                 ];

        public HomeController()
        {


        }
        [Route("/")]
        public IActionResult Index()
        {

            throw new Exception(" Test use Exception Handler Middleware");

            return View(_ProductsList);
        }
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            var product = _ProductsList.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new InvalidProductIdException($"Invalid product ID {id}.");
            }
            return View(product);
        }
        [Route("Error")]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>(); // Get the exception details from the HttpContext
            if (exceptionDetails != null&&exceptionDetails.Error != null) {

                ViewBag.ErrorMessage = exceptionDetails.Error.Message; // Pass the error message to the view using ViewBag

            }
            return View();
        }
    }
}

