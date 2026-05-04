
using Configurations.OptionPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        // private readonly MyApiKeyOptions _configuration;
        private readonly MyApiKeyOptions _options;
        public HomeController(IOptions<MyApiKeyOptions> mykeyoptions) //IConfiguration configuration)
        {
            //  _configuration = configuration;
            _options = mykeyoptions.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            // ViewBag.MyKey= _configuration["My Key"];
            //  ViewBag.MyTEST= _configuration.GetValue<string>("x", "TEST"); // If Key Not Exists set default value 
            #region Hierarchcal Confirguration 
            //ViewBag.ClientId = _configuration["MyApiKey:ClientId"];
            //ViewBag.ClientSecret = _configuration["MyApiKey:ClientSecret"];

            //ViewBag.ClientId = _configuration.GetSection("MyApiKey")["ClientId"];
            //ViewBag.ClientSecret = _configuration.GetSection("MyApiKey")["ClientSecret"];

            //IConfiguration configuration=_configuration.GetSection("MyApiKey");

            //  ViewBag.ClientId = configuration["ClientId"];


            //  ViewBag.ClientSecret = configuration["ClientSecret"];



            #endregion
            #region Options Pattern 
            //  MyApiKeyOptions options = _configuration.GetSection("MyApiKey").Get<MyApiKeyOptions>();

            // Bind :Load Configuration Data To Object Properties should create new object 
            //MyApiKeyOptions options = new MyApiKeyOptions();
            //_configuration.GetSection("MyApiKey").Bind(options);

            //ViewBag.ClientId = options.ClientId;


            //ViewBag.ClientSecret = options.ClientSecret;

            #endregion
                ViewBag.ClientId = _options.ClientId;
                ViewBag.ClientSecret = _options.ClientSecret;

            return View();
        }
    }
}


