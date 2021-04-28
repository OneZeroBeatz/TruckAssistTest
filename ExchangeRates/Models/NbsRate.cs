namespace ExchangeRates.Models.ResponseObjects
{
    public class NbsRate
    {
        public string Code { get; set; }
        public string Date { get; set; }
        public string Date_from { get; set; }
        public string Number { get; set; }
        public string Parity { get; set; }
        public string Cash_buy { get; set; }
        public string Cash_sell { get; set; }
        public string Exchange_buy { get; set; }
        public decimal Exchange_middle { get; set; }
        public string Exchange_sell { get; set; }
    }
}
