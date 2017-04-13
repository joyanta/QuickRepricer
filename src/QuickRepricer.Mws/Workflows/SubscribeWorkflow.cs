using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QuickRepricer.Catalogue.Core.Model;
using QuickRepricer.Catalogue.Persistance;
using QuickRepricer.Messages.ReqRes;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Mws.Amazon.MarketplaceWebService;
using QuickRepricer.Mws.Amazon.MarketplaceWebService.Model;
using QuickRepricer.Mws.Workflows.Spec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace QuickRepricer.Mws.Workflows
{
    public class SubscribeWorkflow : IWorkflow
    {
        private MessagingConfiguration _messageConfiguration;
        private SubscribeRequest _subscribeRequest;
        private IConfigurationRoot _configRoot;
        private IDbContext _dbContext;

        public SubscribeWorkflow(SubscribeRequest subscribeRequest, 
            MessagingConfiguration messageConfiguration, 
            IConfigurationRoot configRoot,
            IDbContext dbContext)
        {
            _subscribeRequest = subscribeRequest;
            _messageConfiguration = messageConfiguration;
            _configRoot = configRoot;
            _dbContext = dbContext;
        }

        public void Run()
        {
            var inventoryReport = GetInventoriesReport(_subscribeRequest.MerchantId, _subscribeRequest.MwsAuthToken, 
                _subscribeRequest.MarketPlaceId);

            SaveInventory(inventoryReport);

            //SendNotification();
        }

        private InventoryReport GetInventoriesReport(string merchantId, string mwsAuthToken, string marketPlaceId)
        {
            
            var config = new MarketplaceWebServiceConfig();
            config.ServiceURL = "https://mws.amazonservices.com";

            var client = new MarketplaceWebServiceClient(
                    _configRoot["MWS:AccessKeyID"],
                    _configRoot["MWS:SecretKey"],
                    _configRoot["MWS:AppName"],
                    _configRoot["MWS:AppVersion"],
                    config);

            var result = GetRequestReportResult(merchantId, mwsAuthToken, marketPlaceId, ReportTypeEnum._GET_MERCHANT_LISTINGS_DATA_, client);

            ReportRequestInfo reportRequestInfo = null;
            while (reportRequestInfo == null || (reportRequestInfo != null && reportRequestInfo.ReportProcessingStatus != ReportProcessingStatusEnum._DONE_.ToString()))
            {
                Thread.Sleep(120000);
                reportRequestInfo = GetReportRequestInfoForReportRequest(result.ReportRequestInfo.ReportRequestId, merchantId,
                    mwsAuthToken, marketPlaceId, client);
            }

            var listings = new Dictionary<string, Item>();

            using (var streamReader = new StreamReader(GetReportStreamById(merchantId, mwsAuthToken,
                reportRequestInfo.GeneratedReportId, client)))
            {
                if (streamReader.Peek() > 0)
                {
                    streamReader.ReadLine(); // read the first line;
                }

                if (streamReader.Peek() > 0)
                {
                    string line = string.Empty;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var lineEntries = line.Split('\t');

                        double price = 0;
                        double.TryParse(lineEntries[4], out price);

                        int quantity = 0;
                        int.TryParse(lineEntries[5], out quantity);

                        listings.Add(lineEntries[2], new Item(lineEntries[0], 
                            lineEntries[2], lineEntries[3], price, quantity, 
                            lineEntries[12], lineEntries[16]));
                    }
                }
            }

            var listingIds = new string[listings.Keys.Count];
            listings.Keys.CopyTo(listingIds, 0);

            //var info = JsonConvert.SerializeObject(new Inventories(merchantId, mwsAuthToken, new List<string>(listingIds)));

            var itemArray = new Item[listings.Values.Count];
            listings.Values.CopyTo(itemArray, 0);

            return new InventoryReport()
            {
                Inventory = new Inventory(merchantId, mwsAuthToken, new List<string>(listingIds)),
                Items = new List<Item>(itemArray)
            };
        }


        private RequestReportResult GetRequestReportResult(string merchantId,
            string mwsAuthToken, string marketplaceId, ReportTypeEnum reportType, MarketplaceWebServiceClient client)
        {
            var reportRequest = new RequestReportRequest();
            reportRequest.Merchant = merchantId;
            reportRequest.MWSAuthToken = mwsAuthToken;
            reportRequest.MarketplaceIdList = new IdList();
            reportRequest.MarketplaceIdList.Id = new List<string>(new string[] { marketplaceId });
            reportRequest.ReportType = reportType.ToString();

            return client.RequestReport(reportRequest).RequestReportResult;
        }

        private ReportRequestInfo GetReportRequestInfoForReportRequest(string reportRequestId,
           string merchantId, string mwsAuthToken,
           string marketplaceId, MarketplaceWebServiceClient client)
        {
            var reportlistReqests = new GetReportRequestListRequest();
            reportlistReqests.WithMerchant(merchantId);
            reportlistReqests.WithMWSAuthToken(mwsAuthToken);

            var reportRequestIdList = new IdList();
            reportRequestIdList.WithId(reportRequestId);
            reportlistReqests.WithReportRequestIdList(reportRequestIdList);

            var reportRequestInfos = client.GetReportRequestList(reportlistReqests)
                .GetReportRequestListResult.ReportRequestInfo;

            if (reportRequestInfos.Count == 0)
            {
                return null;
            }
            return reportRequestInfos[0];
        }


        private Stream GetReportStreamById(string merchantId, string mwsAuthToken,
            string reportId, MarketplaceWebServiceClient client)
        {
            var request = new GetReportRequest();
            request.Merchant = merchantId;
            request.MWSAuthToken = mwsAuthToken;
            request.ReportId = reportId;
            request.Report = new MemoryStream();

            try
            {
                client.GetReport(request);
            }
            catch (MarketplaceWebServiceException ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
                Console.WriteLine("ResponseHeaderMetadata: " + ex.ResponseHeaderMetadata);
            }

            return request.Report;
        }

        //private void SaveInventory(Inventories inventories)
        //{
        //    _dbContext.InsertOrUpdateInventories(inventories);
        //}


        private void SaveInventory(InventoryReport invenotry)
        {
            _dbContext.InsertOrUpdateInventory(invenotry.Inventory);
        }

        private void SendNotification()
        {
            //var items = new List<CatalogueChangeEvent>
            //{
            //    new CatalogueChangeEvent(asim:"9493614a-c1ad-4706-94db-e795aba905e3", name: "Calc", price: 30),
            //    new CatalogueChangeEvent(asim:"eff9c43c-e82e-4972-b9ed-846cab4d4e9c", name: "Motor Bike", price: 35 ),
            //    new CatalogueChangeEvent(asim:"82add376-4d62-48bb-bdd2-a51a955ba738", name: "Hoover", price: 34)
            //};

            //var repricedEvent = new ChangeEventsCollection<CatalogueChangeEvent>(items);

            //var queue = MessageQueueFactory.CreateOutbound("repriced-event", 
            //    MessagePattern.PublishSubscribe, _messageConfiguration);

            //queue.Send(new Message
            //{
            //    Body = repricedEvent
            //});

            Thread.Sleep(1000);
        }
    }
}
