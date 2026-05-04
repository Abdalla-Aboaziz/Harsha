using Autofac;
using Di_Contract;
using DiService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICiteiesServices _citiesServer1;
        private readonly ICiteiesServices _citiesServer2;
        private readonly ICiteiesServices _citiesServer3;
        //  private readonly IServiceScopeFactory _serviceScopeFactory; // Create Service Scope in Ioc Container
        private readonly ILifetimeScope _lifetimeScope; // Create Service Scope in  Autofac Ioc

        public HomeController(ICiteiesServices citiesServer1, ILifetimeScope serviceScopeFactory, ICiteiesServices citiesServer2, ICiteiesServices citiesServer3)
        {
            // Object from the IOC Container
            _citiesServer1 = citiesServer1;
            _citiesServer2 = citiesServer2;
            _citiesServer3 = citiesServer3;
            _lifetimeScope = serviceScopeFactory;

        }
        [Route("/")]
        public IActionResult Index()
        {
            var cities = _citiesServer1.GetCities();
            ViewBag.Service1Id = _citiesServer1.ServiceInstanceId;
            ViewBag.Service2Id = _citiesServer2.ServiceInstanceId;
            ViewBag.Service3Id = _citiesServer3.ServiceInstanceId;
            // Service Scope 
          //  using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            using (ILifetimeScope scope = _lifetimeScope.BeginLifetimeScope()) //Autofac
            {
                // Inject Cities Service 
                ICiteiesServices citeiesServices=
                scope.Resolve<ICiteiesServices>();
                // Db Work 
                ViewBag.InstanceId_CitiesService_INScope = citeiesServices.ServiceInstanceId;
            } // End of Scope ; it Call CitiesSerice.Dispose()

                return View(cities);
        }
    }
}


////////////////////// Traient //////////////////////
/*  كل مرة يتم طلب خدمة من ال IOC Container يتم انشاء نسخة جديدة منها و هذا ما يفسر اختلاف ال GUID لكل نسخة
 *
17bab6bc-a02b-4162-9f83-5f5accfbcd65
fb90c7ac-a3ae-48e3-822d-b0aafe50d336
a712118a-c9af-4a37-8466-0970445877e2
*/
///////////////// Scoped ////////////////////////
/* كل مرة يتم طلب خدمة من ال IOC Container يتم انشاء نسخة جديدة منها و لكن في نفس الطلب يتم اعادة نفس النسخة و هذا ما يفسر تطابق ال GUID لكل نسخة في نفس الطلب و اختلافه بين الطلبات
 * Request 1 
 
53dd2c39-9ce6-4671-af77-3515dbd22218
53dd2c39-9ce6-4671-af77-3515dbd22218
53dd2c39-9ce6-4671-af77-3515dbd22218
 
Request 2
dc08655b-416d-475b-8ab0-32de7fa38e41
dc08655b-416d-475b-8ab0-32de7fa38e41
dc08655b-416d-475b-8ab0-32de7fa38e41
 */

///////////////// Singleton ////////////////////////
/*  نفس الاوبجكت لحد ما اقفل ال برنامج 
 * Request 1 

7e349d38-fda4-472e-bd70-7fd4ec307c2b
7e349d38-fda4-472e-bd70-7fd4ec307c2b
7e349d38-fda4-472e-bd70-7fd4ec307c2b
 
Request 2
7e349d38-fda4-472e-bd70-7fd4ec307c2b
7e349d38-fda4-472e-bd70-7fd4ec307c2b
7e349d38-fda4-472e-bd70-7fd4ec307c2b

 */