namespace ConfigurationStocksApp_HttpClient_.ServiceContract
{
    public interface IFinhubService
    {
       Task<Dictionary<string, object>?> GetStockPriceQuote(string symbol);
    }
}
