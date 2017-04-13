namespace QuickRepricer.Core.Services.Repricer.Messages
{
    public class PriceUpdateMessage
    {
        public string ASIM { get; private set; }
        public string MerchantSku { get; private set; }
        public double CurrentPrice { get; private set; }

        public PriceUpdateMessage(string asim, string merchantSku, double currentPrice)
        {
            ASIM = asim;
            MerchantSku = merchantSku;
            CurrentPrice = currentPrice;
        }
    }
}
