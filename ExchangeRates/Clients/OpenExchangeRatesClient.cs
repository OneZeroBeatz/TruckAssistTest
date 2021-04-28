using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRates.Clients
{
    public class OpenExchangeRatesClient
    {
        private readonly string ApiKey;
        private readonly string ApiUrl;

        public OpenExchangeRatesClient(string apiKey, string apiUrl)
        {
            ApiKey = apiKey;
            ApiUrl = apiUrl;
        }

        public async Task<HttpResponseMessage> GetUsdExchangeRatesFor(string symbols)
        {
            string url = $"{ApiUrl}latest.json?app_id={ApiKey}&symbols={symbols}";
            var httpClient = HttpClientFactory.Create();
            var httpResponseMessage = await httpClient.GetAsync(url);
            return httpResponseMessage;

        }

        public async Task<HttpResponseMessage> GetUsdOhlcExchangeRatesFor(DateTime startDate, string period, string symbols)
        {
            string url = $"{ApiUrl}ohlc.json?app_id={ApiKey}&start={startDate.ToShortDateString()}&period={period}&symbols={symbols}";
            var httpClient = HttpClientFactory.Create();
            var httpResponseMessage = await httpClient.GetAsync(url);
            return httpResponseMessage;
        }
    }
}
