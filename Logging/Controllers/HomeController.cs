
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;

namespace Logging.Controllers
{
    public class HomeController : Controller
    {
     

        public HomeController()
        {
         

        }
        [Route("/")]
        public IActionResult Index()
        {
            

                return View();
        }
    }
}

