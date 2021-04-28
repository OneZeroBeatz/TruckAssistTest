using ExchangeRates.Clients;
using ExchangeRates.Models;
using ExchangeRates.Models.Entities;
using ExchangeRates.Models.ResponseObjects;
using ExchangeRates.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public class NbsExchangeRateService
    {
        private readonly NbsResenjeClient _exchangeRateClient;
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private string BaseCurrencySymbol => "USD";

        public NbsExchangeRateService(NbsResenjeClient exchangeRateClient,
                                   IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateClient = exchangeRateClient;
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task CompareExchangeRates()
        {
            var todayRatesResponseMessage = await _exchangeRateClient.GetTodayRates();

            if (todayRatesResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = todayRatesResponseMessage.Content;
                var nbsExchangeRates = await content.ReadAsAsync<NbsExchangeRates>();

                var oneUsdToRsdRate = nbsExchangeRates.Rates.First(rate => rate.Code.Equals("USD")); //TODO; FirstOrDefault and add validation
                var oneEurToRsdRate = nbsExchangeRates.Rates.First(rate => rate.Code.Equals("EUR"));
                var oneGbpToRsdRate = nbsExchangeRates.Rates.First(rate => rate.Code.Equals("GBP"));

                var oneUsdToEurRate = oneUsdToRsdRate.Exchange_middle / oneEurToRsdRate.Exchange_middle;
                var oneUsdToGbpRate = oneUsdToRsdRate.Exchange_middle / oneGbpToRsdRate.Exchange_middle;

                var openExchangeRate = _exchangeRateRepository.GetAll(); //TODO Allow get by day and currency, not all

                var oneUsdToEurOpenExchangeRate = openExchangeRate.First(rate => rate.RateCurrency.Equals("EUR")); //TODO Need to make sure its today's
                var oneUsdToGbpOpenExchangeRate = openExchangeRate.First(rate => rate.RateCurrency.Equals("GBP"));

                Console.WriteLine($"NBS(middle) - 1 USD = {oneUsdToEurRate} EUR");
                Console.WriteLine($"NBS(middle) - 1 USD = {oneUsdToGbpRate} GBP");
                Console.WriteLine($"Open - 1 USD = {oneUsdToEurOpenExchangeRate.Rate} EUR");
                Console.WriteLine($"Open - 1 USD = {oneUsdToGbpOpenExchangeRate.Rate} GBP");
            }
        }
    }
}
