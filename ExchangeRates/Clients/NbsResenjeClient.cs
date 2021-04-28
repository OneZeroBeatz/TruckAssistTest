using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRates.Clients
{
    public class NbsResenjeClient
    {
        private readonly string ApiUrl;

        public NbsResenjeClient(string apiUrl)
        {
            ApiUrl = apiUrl;
        }

        public async Task<HttpResponseMessage> GetTodayRates()
        {
            string url = $"{ApiUrl}rates/today";
            var httpClient = HttpClientFactory.Create();
            var httpResponseMessage = await httpClient.GetAsync(url);
            return httpResponseMessage;

        }
    }
}
