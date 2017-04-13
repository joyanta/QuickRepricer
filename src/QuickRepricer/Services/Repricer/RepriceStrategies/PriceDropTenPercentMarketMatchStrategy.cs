using QuickRepricer.Core.Services.Repricer.Messages;

namespace QuickRepricer.Services.Repricer.RepriceStrategies
{
    public class PriceDropTenPercentMarketMatchStrategy : RepriceStrategy
    {
        public override string Type
        {
            get
            {
                return "PriceDropTenPercentMarketMatchStrategy";
            }
        }

        public override double Reprice(RepricedMessage repricedMessage)
        {
            return (repricedMessage.MarketPrice * 0.9);
        }
    }
}
