using QuickRepricer.Core.Services.Repricer.Messages;

namespace QuickRepricer.Services.Repricer.RepriceStrategies
{
    public abstract class RepriceStrategy
    {
        public abstract string Type { get; }
        public abstract double Reprice(RepricedMessage repricedMessage);
    }
}
