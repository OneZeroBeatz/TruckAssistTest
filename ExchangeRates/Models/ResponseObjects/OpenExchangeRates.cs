using System.Collections.Generic;

namespace ExchangeRates.Models.ResponseObjects
{
    public class OpenExchangeRates
    {
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }

    }
}
