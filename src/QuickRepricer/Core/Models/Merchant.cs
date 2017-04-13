using System.Collections.Generic;
namespace QuickRepricer.Core.Models
{
    public class Merchant
    {
        public int MerchantId { get; set; }

        public string SellerId { get; set; }

        public string SecretKey { get; set; }

        public string AmazoneMWSAuthToken { get; set; }

        public virtual ICollection<MarketPlace> MarketPlaces { get; set; }
    }
}
