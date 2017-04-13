using QuickRepricer.Core.Services.Repricer.Messages;

namespace QuickRepricer.Services.Repricer.RepriceStrategies
{
    public class PriceDropMarketMatchStrategy : RepriceStrategy
    {
        public override string Type
        {
            get
            {
                return "PriceDropMarketMatchStrategy";
            }
        }

        public override double Reprice(RepricedMessage repricedMessage)
        {
            // 
           
            return repricedMessage.MarketPrice;
        }
    }
}
