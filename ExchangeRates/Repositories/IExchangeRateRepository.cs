using ExchangeRates.Models.Entities;

namespace ExchangeRates.Repositories
{
    public interface IExchangeRateRepository
    {
        void Add(ExchangeRate exchangeRate);
    }
}