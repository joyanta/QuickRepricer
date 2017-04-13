namespace QuickRepricer.Core.Services.Repricer.Messages
{
    public class RepricedMessage
    {
        public string ASIM { get; private set; }
        public string MerchantSku { get; private set;  }
        public double CurrentPrice { get; private set; }
        public double MarketPrice { get; private set; }
        public string Strategy { get; private set; }

        public RepricedMessage(string asim, string merchantSku,
            double currentPrice, double marketPrice, string strategy)
        {
            ASIM = asim;
            MerchantSku = merchantSku;
            CurrentPrice = currentPrice;
            MarketPrice = marketPrice;
            Strategy = strategy;
        }
    }
}
