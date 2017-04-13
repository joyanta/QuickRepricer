namespace QuickRepricer.Core.Services.Repricer.Payloads
{
    public class MerchantMap
    {
        public string MerchantSku { get; private set; }
        public double CurrentPrice { get; private set; }
        public string Strategy { get; private set; }

        public MerchantMap(string merchantSku, double currentPrice, string strategy)
        {
            MerchantSku = merchantSku;
            CurrentPrice = currentPrice;
            Strategy = strategy;
        }
    }

}
