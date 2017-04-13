namespace QuickRepricer.Core.Services.Repricer.Messages
{
    public class StrategyUpdateMessage 
    {
        public string ASIM { get; private set; }
        public string MerchantSku { get; private set; }
        public string Strategy { get; private set; }

        public StrategyUpdateMessage(string asim, string merchantSku, string strategy)
        {
            ASIM = asim;
            MerchantSku = merchantSku;
            Strategy = strategy;
        }
    }
}
