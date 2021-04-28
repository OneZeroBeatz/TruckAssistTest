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

        public async Task<HttpResponseMessage> GetExchangeRatesFor(string @base, string symbols)
        {
            string url = $"{ApiUrl}latest.json?app_id={ApiKey}&base={@base}&symbols={symbols}";
            var httpClient = HttpClientFactory.Create();
            var httpResponseMessage = await httpClient.GetAsync(url);
            return httpResponseMessage;

        }

        public async Task<HttpResponseMessage> GetOhlcExchangeRatesFor(string @base, DateTime startDate, string period, string symbols)
        {
            string url = $"{ApiUrl}ohlc.json?app_id={ApiKey}&base={@base}&start={startDate.ToString("YYYY-MM-DDThh:mm:00Z")}&period={period}&symbols={symbols}&show_alternative=1";
            var httpClient = HttpClientFactory.Create();
            var httpResponseMessage = await httpClient.GetAsync(url);
            return httpResponseMessage;
        }
    }
}
