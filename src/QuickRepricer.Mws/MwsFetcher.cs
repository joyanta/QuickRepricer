using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickRepricer.Catalogue.Core.Model;
using QuickRepricer.Catalogue.Persistance;
using QuickRepricer.Mws.Amazon.MarketplaceWebServiceProducts;
using QuickRepricer.Mws.Amazon.MarketplaceWebServiceProducts.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuickRepricer.Mws
{
    public class MwsFetcher
    {
        private CancellationTokenSource _cancellationTokenSource;

        private IServiceProvider _serviceProvider;

        private MarketplaceWebServiceProductsClient _client;

        private Dictionary<string, decimal> _PriceLookUp = new Dictionary<string, decimal>();

        public MwsFetcher(IServiceCollection serviceCollection)
        {
            _serviceProvider = serviceCollection.BuildServiceProvider();
            var configRoot = _serviceProvider.GetService<IConfigurationRoot>();
            
            // needs to come from the database;
            string serviceURL = "https://mws.amazonservices.com";
            var marketplaceWebServiceProductsConfig = new MarketplaceWebServiceProductsConfig();
            marketplaceWebServiceProductsConfig.ServiceURL = serviceURL;

            _client = new MarketplaceWebServiceProductsClient(configRoot["MWS:AppName"], 
                configRoot["MWS:AppVersion"],
                configRoot["MWS:AccessKeyID"], 
                configRoot["MWS:SecretKey"], 
                marketplaceWebServiceProductsConfig);
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(async () =>
            {
                await StartFetchingDataforMerchants();
            },
            _cancellationTokenSource.Token);
        }

        public void Stop()
        {


        }

        private async Task StartFetchingDataforMerchants()
        {
            var rethinkDbConnectionFactory = _serviceProvider.GetService<IRethinkDbConnectionFactory>();
            
            var curse = await rethinkDbConnectionFactory.CurseThroughTable<Inventory>("MerchantListings");

            while ( await curse.MoveNextAsync())
            {
                // go through each merchant and uisng dtol data flow send requests
                // for a bluck of ASINS for that maerchant;

                var sellerId = "A2VJ4SRVLLF6SA";
                var mwsAuthToken = "amzn.mws.faac2bdf-b67b-1a4b-f4cc-e474ab8e1f21";
                var marketplaceId = "ATVPDKIKX0DER";
                var asins = new List<string>() { "B00FSA541M" };

                FetchingCompetativePrices(sellerId, mwsAuthToken, marketplaceId, asins);
            }
        }
       


        private void FetchingCompetativePrices(string sellerId, string mwsAuthToken,
            string marketplaceId, List<string> asins)
        {
            try
            {
                var response = InvokeGetCompetitivePricingForASIN(sellerId, mwsAuthToken, marketplaceId, asins);
                
                foreach (var pricingsForAnAsin in response.GetCompetitivePricingForASINResult)
                {
                    // can't really use it;
                    //var lowestPrice = pricing.Product.CompetitivePricing.CompetitivePrices.CompetitivePrice.FirstOrDefault();

                    decimal currentLowestLandedPrice = -1;
                    foreach (var eachPriceForThisASIN in pricingsForAnAsin.Product.CompetitivePricing.CompetitivePrices.CompetitivePrice)
                    {
                        var landedPrice = eachPriceForThisASIN.Price.LandedPrice.Amount;

                        if (landedPrice < currentLowestLandedPrice)
                        {
                            currentLowestLandedPrice = landedPrice;
                        }

                    }

                    // we have the lower price; 
                    if (_PriceLookUp.ContainsKey(pricingsForAnAsin.ASIN))
                    {
                        _PriceLookUp[pricingsForAnAsin.ASIN] = currentLowestLandedPrice;
                    }
                    else
                    {

                        _PriceLookUp.Add(pricingsForAnAsin.ASIN, currentLowestLandedPrice);
                    }
                }

            }
            catch (MarketplaceWebServiceProductsException ex)
            {
                // Exception properties are important for diagnostics.
                Amazon.MarketplaceWebServiceProducts.Model.ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                Console.WriteLine("Service Exception:");
                if (rhmd != null)
                {
                    Console.WriteLine("RequestId: " + rhmd.RequestId);
                    Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                }
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("StatusCode: " + ex.StatusCode);
                Console.WriteLine("ErrorCode: " + ex.ErrorCode);
                Console.WriteLine("ErrorType: " + ex.ErrorType);
                throw ex;
            }

        }

        private GetCompetitivePricingForASINResponse InvokeGetCompetitivePricingForASIN(string sellerId, string mwsAuthToken, 
            string marketplaceId, List<string> asins)
        {
            // Create a request.
            GetCompetitivePricingForASINRequest request = new GetCompetitivePricingForASINRequest();

            request.SellerId = sellerId;
            request.MWSAuthToken = mwsAuthToken;
            request.MarketplaceId = marketplaceId;
            var asinList = new ASINListType();
            asinList.ASIN = asins; 
            request.ASINList = asinList;
            return _client.GetCompetitivePricingForASIN(request);
        }
        
    }
}
