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

        public ExchangeRateService(OpenExchangeRatesClient exchangeRateClient,
                                   IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateClient = exchangeRateClient;
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task AddUsdExchangeRatesFor(string [] currencies)
        {
            var httpResponseMessage = await _exchangeRateClient.GetUsdExchangeRatesFor(string.Join(',', currencies));

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = httpResponseMessage.Content;
                var currencyExchangeRates = await content.ReadAsAsync<CurrencyExchangeRates>();
                foreach (var rate in currencyExchangeRates.Rates)
                {
                    _exchangeRateRepository.Add(new ExchangeRate("USD", rate.Key, rate.Value, DateTime.Now));
                }
            }
            else
            {
                Console.WriteLine($"Cannot get exchange rates for USD to currencies: {string.Join(',', currencies)}, StatusCode: {httpResponseMessage.StatusCode}, Reason: {httpResponseMessage.ReasonPhrase}");
            }
        }
       
    }
}
