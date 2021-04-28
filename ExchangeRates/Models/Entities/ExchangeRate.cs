using System;

namespace ExchangeRates.Models.Entities
{
    public class ExchangeRate
    {
        public string BaseCurrency { get; }
        public string RateCurrency { get; }
        public decimal Rate { get; }
        public DateTime DateTime { get; }

        public ExchangeRate(string baseCurrency, string rateCurrency, decimal rate, DateTime dateTime)
        {
            BaseCurrency = baseCurrency;
            RateCurrency = rateCurrency;
            Rate = rate;
            DateTime = dateTime;
        }
    }
}
