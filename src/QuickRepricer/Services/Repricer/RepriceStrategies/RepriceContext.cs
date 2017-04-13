using QuickRepricer.Core.Services.Repricer.Messages;

namespace QuickRepricer.Services.Repricer.RepriceStrategies
{
    public class RepriceContext
    {
        public RepriceStrategy RepriceStrategy { get; private set; }
        public RepricedMessage RepricedMessage { get; private set; }

        public RepriceContext(RepriceStrategy repriceStrategy, 
            RepricedMessage repricedMessage)
        {
            RepriceStrategy = repriceStrategy;
            RepricedMessage = repricedMessage;
         
        }

        public double Reprice()
        {
            return RepriceStrategy.Reprice(RepricedMessage);
        }
    }
}


