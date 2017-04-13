using Newtonsoft.Json.Linq;
using QuickRepricer.Services.Repricer.RepriceStrategies;
using System;
namespace QuickRepricer.Services.Repricer.Helpers
{
    public class RepriceStrategyCreationConverter : JsonCreationConverter<RepriceStrategy>
    {
        protected override RepriceStrategy Create(Type objectType, JObject jsonObject)
        {
            string typeName = (jsonObject["Type"]).ToString();
            switch (typeName)
            {
                case "PriceDropMarketMatchStrategy":
                    return new PriceDropMarketMatchStrategy();
                case "PriceDropTenPercentMarketMatchStrategy":
                    return new PriceDropTenPercentMarketMatchStrategy();
                default:
                    return new PriceDropMarketMatchStrategy();
            }
        }
    }
}
