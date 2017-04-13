using System.Collections.Generic;

namespace QuickRepricer.Messages.ReqRes
{
    public class SubscribeRequest
    {
        public string UserName { get; private set; }
        public string MerchantId { get; private set; }
        public string MarketPlaceId { get; private set; }
        public string MwsAuthToken { get; private set; }

        public SubscribeRequest(string userName, string merchantId, 
            string marketPlaceId, string mwsAuthToken)
        {
            UserName = userName;
            MerchantId = merchantId;
            MarketPlaceId = marketPlaceId;
            MwsAuthToken = mwsAuthToken;
        }
    }

    public class SubscribeStartResponse
    {
        public string Status { get; private set; }

        public SubscribeStartResponse(string status)
        {
            Status = status;
        }
    }


    public class SubscribeFinishedResponse
    {
        public IEnumerable<string> Items { get; private set; }

        public SubscribeFinishedResponse(IEnumerable<string> items)
        {
            Items = items;
        }
    }

}
