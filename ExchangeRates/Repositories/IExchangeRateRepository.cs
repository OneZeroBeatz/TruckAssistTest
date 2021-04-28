using ExchangeRates.Models.Entities;
using System.Collections.Generic;

namespace ExchangeRates.Repositories
{
    public interface IExchangeRateRepository
    {
        void Add(ExchangeRate exchangeRate);
        IEnumerable<ExchangeRate> GetAll();
    }
}