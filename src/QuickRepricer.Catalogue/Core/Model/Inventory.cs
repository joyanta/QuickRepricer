using System.Collections.Generic;

namespace QuickRepricer.Catalogue.Core.Model
{
    public class Inventory
    {
        public string MerchantId { get; private set; }
        public string MwsAuthToken { get; private set; }
          
        //public Dictionary<string, Item> Listing { get; private set; }
        
        public List<string> Listing { get; set; }

        public Inventory(string merchantId, string mwsAuthToken, List<string> listing)
        {
            MerchantId = merchantId;
            MwsAuthToken = mwsAuthToken;
            Listing = listing;
        }
    }
}
