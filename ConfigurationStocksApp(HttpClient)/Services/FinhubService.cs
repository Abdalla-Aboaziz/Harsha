using ConfigurationStocksApp_HttpClient_.ServiceContract;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationStocksApp_HttpClient_.Servicecs
{
    public class FinhubService: IFinhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinhubService(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
           _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string symbol)
        {
            using (HttpClient httpClient= _httpClientFactory.CreateClient())
            {
                // HttpRequestMessage : Represent The Http Request That We Want To Send To The Server
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {

                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["FinhubApiKey"]}")// release symbol

                }; 
                // SendAsync : Send The Http Request To The Server And Get The Http Response From The Server
                HttpResponseMessage httpResponseMessage =await httpClient.SendAsync(httpRequestMessage);
                // ReadAsStreamAsync : Read The Http Response Content As Stream
                Stream stream =   httpResponseMessage.Content.ReadAsStream();
                // StreamReader : Read The Stream And Convert It To String
                StreamReader streamReader = new StreamReader(stream);

              string response = await streamReader.ReadToEndAsync();
                Dictionary<string, object>? ResponseDictionry =
                JsonSerializer.Deserialize<Dictionary<string, object>>(response);
                if (ResponseDictionry == null)
                {
                    throw new InvalidOperationException("Failed to retrieve stock price quote.");
                }
                if(ResponseDictionry.ContainsKey("error"))
                {
                    throw new InvalidOperationException($"Error from API: {ResponseDictionry["error"]}");
                }

                return ResponseDictionry;
            }
           

        }

      
    }
}

//  Should Enable User Secret In This Project To Store The API Key Instead Of Hardcoding It In The Code
// open Command Prompt And Navigate To The Project Directory Then Run The Following Command To Enable User Secret
// dotnet user-secrets init
// After Enabling User Secret You Can Store The API Key In User Secret By Running The Following Command
// dotnet user-secrets set "FinhubApiKey" "d7r67ihr01qtpsm17pi0d7r67ihr01qtpsm17pig"

