using ExchangeRates.Clients;
using ExchangeRates.Models;
using ExchangeRates.Models.Entities;
using ExchangeRates.Models.ResponseObjects;
using ExchangeRates.Repositories;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public class ExchangeRateService
    {
        private readonly OpenExchangeRatesClient _exchangeRateClient;
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private string BaseCurrencySymbol => "USD";

        public ExchangeRateService(OpenExchangeRatesClient exchangeRateClient,
                                   IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateClient = exchangeRateClient;
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task AddExchangeRatesFor(string [] currencies)
        {
            var httpResponseMessage = await _exchangeRateClient.GetExchangeRatesFor(BaseCurrencySymbol, string.Join(',', currencies));

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = httpResponseMessage.Content;
                var currencyExchangeRates = await content.ReadAsAsync<CurrencyExchangeRates>();
                foreach (var rate in currencyExchangeRates.Rates)
                {
                    _exchangeRateRepository.Add(new ExchangeRate(BaseCurrencySymbol, rate.Key, rate.Value, DateTime.Now));
                }
            }
            else
            {
                Console.WriteLine($"Cannot get exchange rates for USD to currencies: {string.Join(',', currencies)}, StatusCode: {httpResponseMessage.StatusCode}, Reason: {httpResponseMessage.ReasonPhrase}");
            }
        }

        public async Task<CurrencyOhlcExchangeRates> GetExchangeRatesForLastSevenDays(string[] currencies)
        {
            var httpResponseMessage = await _exchangeRateClient.GetOhlcExchangeRatesFor(BaseCurrencySymbol, DateTime.Now.AddDays(-7), "7d", string.Join(',', currencies));

            if (httpResponseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Cannot get exchange rates for last seven days, StatusCode: {httpResponseMessage.StatusCode}, Reason: {httpResponseMessage.ReasonPhrase}");
                return null;
            }

            var content = httpResponseMessage.Content;
            var currencyExchangeRates = await content.ReadAsAsync<CurrencyOhlcExchangeRates>();


            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            return currencyExchangeRates;
        }
    }
}
