using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Models
{
    public class CurrencyOhlcExchangeRates
    {
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public Dictionary<string, Ohlc> OhlcRates { get; set; }
    }
}
