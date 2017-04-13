using System.Collections.Generic;
namespace QuickRepricer.Core.Services.Repricer.Payloads
{
    public class AsimSkuMap
    {
        public string Asim { get; set; }
        public List<MerchantMap> MerchantMaps { get; set; }
    }
}
