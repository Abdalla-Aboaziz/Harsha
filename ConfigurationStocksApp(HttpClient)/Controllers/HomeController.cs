using ConfigurationStocksApp_HttpClient_.Models;
using ConfigurationStocksApp_HttpClient_.Servicecs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ConfigurationStocksApp_HttpClient_.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinhubService _finhubService;
        //  private readonly IConfiguration _configuration;
        private readonly IOptions<TradingOptions> _options;


        public HomeController(FinhubService finhubService, IOptions<TradingOptions> options)//IConfiguration configuration)
        {
         _finhubService = finhubService;
         _options = options;
         //_configuration = configuration;
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if(_options.Value.DefaultStockSymbol == null)
            {
               _options.Value.DefaultStockSymbol = "MSFT";
            }
            // Dictionary<string, object>? stockQuote = await _finhubService.GetStockPriceQuote($"{_configuration["TradingOptions:DefaultStockSymbol"]}");
            Dictionary<string, object>? stockQuote = await _finhubService.GetStockPriceQuote(_options.Value.DefaultStockSymbol);
            Stock stock = new Stock()
            {
                StockSymbol = _options.Value.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(stockQuote["c"].ToString()),
                LowestPrice = Convert.ToDouble(stockQuote["l"].ToString()),
                HighestPrice = Convert.ToDouble(stockQuote["h"].ToString()),
                OpenPrice = Convert.ToDouble(stockQuote["o"].ToString())
            };
            return View(stock);
        }
    }
}
