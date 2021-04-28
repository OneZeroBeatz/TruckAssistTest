using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Models
{
    public class Ohlc
    {     
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Average { get; set; }
    }
}
