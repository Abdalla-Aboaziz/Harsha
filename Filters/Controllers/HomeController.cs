
using Filters.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;

namespace Filters.Controllers
{
    [ServiceFilter(typeof(AuditActionFilter))]
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
       
        [Route("/")]
        public IActionResult Index()
        {
            logger.LogInformation("Executing Index action on HomeController");

            return View();
        }
    }
}

