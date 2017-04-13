/*******************************************************************************
 * Copyright 2009-2016 Amazon Services. All Rights Reserved.
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 *
 * You may not use this file except in compliance with the License. 
 * You may obtain a copy of the License at: http://aws.amazon.com/apache2.0
 * This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 * CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 * specific language governing permissions and limitations under the License.
 *******************************************************************************
 * MWS Merchant Fulfillment Service
 * API Version: 2015-06-01
 * Library Version: 2016-03-30
 * Generated: Tue Mar 29 18:59:58 UTC 2016
 */


using QuickRepricer.Mws.Amazon.MWSMerchantFulfillmentService.Model;
using System;

namespace QuickRepricer.Mws.Amazon.MWSMerchantFulfillmentService
{

    /// <summary>
    /// Runnable sample code to demonstrate usage of the C# client.
    ///
    /// To use, import the client source as a console application,
    /// and mark this class as the startup object. Then, replace
    /// parameters below with sensible values and run.
    /// </summary>
    public class MWSMerchantFulfillmentServiceSample
    {

        public static void Main1(string[] args)
        {
            // TODO: Set the below configuration variables before attempting to run

            // Developer AWS access key
            string accessKey = "replaceWithAccessKey";

            // Developer AWS secret key
            string secretKey = "replaceWithSecretKey";

            // The client application name
            string appName = "CSharpSampleCode";

            // The client application version
            string appVersion = "1.0";

            // The endpoint for region service and version (see developer guide)
            // ex: https://mws.amazonservices.com
            string serviceURL = "replaceWithServiceURL";

            // Create a configuration object
            MWSMerchantFulfillmentServiceConfig config = new MWSMerchantFulfillmentServiceConfig();
            config.ServiceURL = serviceURL;
            // Set other client connection configurations here if needed
            // Create the client itself
            MWSMerchantFulfillmentService client = new MWSMerchantFulfillmentServiceClient(accessKey, secretKey, appName, appVersion, config);

            MWSMerchantFulfillmentServiceSample sample = new MWSMerchantFulfillmentServiceSample(client);

            // Uncomment the operation you'd like to test here
            // TODO: Modify the request created in the Invoke method to be valid

            try 
            {
                IMWSResponse response = null;
                // response = sample.InvokeCancelShipment();
                // response = sample.InvokeCreateShipment();
                // response = sample.InvokeGetEligibleShippingServices();
                // response = sample.InvokeGetShipment();
                // response = sample.InvokeGetServiceStatus();
                Console.WriteLine("Response:");
                ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
                // We recommend logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
            }
            catch (MWSMerchantFulfillmentServiceException ex)
            {
                // Exception properties are important for diagnostics.
                ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                Console.WriteLine("Service Exception:");
                if(rhmd != null)
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

        private readonly MWSMerchantFulfillmentService client;

        public MWSMerchantFulfillmentServiceSample(MWSMerchantFulfillmentService client)
        {
            this.client = client;
        }

        public CancelShipmentResponse InvokeCancelShipment()
        {
            // Create a request.
            CancelShipmentRequest request = new CancelShipmentRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            string shipmentId = "example";
            request.ShipmentId = shipmentId;
            return this.client.CancelShipment(request);
        }

        public CreateShipmentResponse InvokeCreateShipment()
        {
            // Create a request.
            CreateShipmentRequest request = new CreateShipmentRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            ShipmentRequestDetails shipmentRequestDetails = new ShipmentRequestDetails();
            request.ShipmentRequestDetails = shipmentRequestDetails;
            string shippingServiceId = "example";
            request.ShippingServiceId = shippingServiceId;
            string shippingServiceOfferId = "example";
            request.ShippingServiceOfferId = shippingServiceOfferId;
            return this.client.CreateShipment(request);
        }

        public GetEligibleShippingServicesResponse InvokeGetEligibleShippingServices()
        {
            // Create a request.
            GetEligibleShippingServicesRequest request = new GetEligibleShippingServicesRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            ShipmentRequestDetails shipmentRequestDetails = new ShipmentRequestDetails();
            request.ShipmentRequestDetails = shipmentRequestDetails;
            return this.client.GetEligibleShippingServices(request);
        }

        public GetShipmentResponse InvokeGetShipment()
        {
            // Create a request.
            GetShipmentRequest request = new GetShipmentRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            string shipmentId = "example";
            request.ShipmentId = shipmentId;
            return this.client.GetShipment(request);
        }

        public GetServiceStatusResponse InvokeGetServiceStatus()
        {
            // Create a request.
            GetServiceStatusRequest request = new GetServiceStatusRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            return this.client.GetServiceStatus(request);
        }


    }
}
