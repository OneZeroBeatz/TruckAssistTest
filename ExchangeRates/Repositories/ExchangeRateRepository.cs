using ExchangeRates.Models.Entities;
using System.Collections.Generic;

namespace ExchangeRates.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly List<ExchangeRate> _exchangeRates;

        public ExchangeRateRepository()
        {
            _exchangeRates = new List<ExchangeRate>();
        }

        public void Add(ExchangeRate exchangeRate)
        {
            _exchangeRates.Add(exchangeRate);
        }

        public IEnumerable<ExchangeRate> GetAll()
        {
            return new List<ExchangeRate>(_exchangeRates);
        }
    }
}
