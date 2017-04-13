/*******************************************************************************
 * Copyright 2009-2015 Amazon Services. All Rights Reserved.
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 *
 * You may not use this file except in compliance with the License. 
 * You may obtain a copy of the License at: http://aws.amazon.com/apache2.0
 * This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 * CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 * specific language governing permissions and limitations under the License.
 *******************************************************************************
 * MWS Subscriptions Service
 * API Version: 2013-07-01
 * Library Version: 2015-06-18
 * Generated: Thu Jun 18 19:27:11 GMT 2015
 */

using System;
using System.IO;
using System.Reflection;
using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;
using QuickRepricer.Mws.Amazon.MWSSubscriptionsService.Model;

namespace QuickRepricer.Mws.Amazon.MWSSubscriptionsService.Mock
{

    /// <summary>
    /// MWSSubscriptionsServiceMock is the implementation of MWSSubscriptionsService based
    /// on the pre-populated set of XML files that serve local data. It simulates
    /// responses from MWS.
    /// </summary>
    /// <remarks>
    /// Use this to test your application without making a call to MWS
    ///
    /// Note, current Mock Service implementation does not validate requests
    /// </remarks>
    public class MWSSubscriptionsServiceMock : MWSSubscriptionsService
    {

        public CreateSubscriptionResponse CreateSubscription(CreateSubscriptionInput request)
        {
            return newResponse<CreateSubscriptionResponse>();
        }

        public DeleteSubscriptionResponse DeleteSubscription(DeleteSubscriptionInput request)
        {
            return newResponse<DeleteSubscriptionResponse>();
        }

        public DeregisterDestinationResponse DeregisterDestination(DeregisterDestinationInput request)
        {
            return newResponse<DeregisterDestinationResponse>();
        }

        public GetSubscriptionResponse GetSubscription(GetSubscriptionInput request)
        {
            return newResponse<GetSubscriptionResponse>();
        }

        public ListRegisteredDestinationsResponse ListRegisteredDestinations(ListRegisteredDestinationsInput request)
        {
            return newResponse<ListRegisteredDestinationsResponse>();
        }

        public ListSubscriptionsResponse ListSubscriptions(ListSubscriptionsInput request)
        {
            return newResponse<ListSubscriptionsResponse>();
        }

        public RegisterDestinationResponse RegisterDestination(RegisterDestinationInput request)
        {
            return newResponse<RegisterDestinationResponse>();
        }

        public SendTestNotificationToDestinationResponse SendTestNotificationToDestination(SendTestNotificationToDestinationInput request)
        {
            return newResponse<SendTestNotificationToDestinationResponse>();
        }

        public UpdateSubscriptionResponse UpdateSubscription(UpdateSubscriptionInput request)
        {
            return newResponse<UpdateSubscriptionResponse>();
        }

        public GetServiceStatusResponse GetServiceStatus(GetServiceStatusRequest request)
        {
            return newResponse<GetServiceStatusResponse>();
        }

        private T newResponse<T>() where T : IMWSResponse {
            Stream xmlIn = null;
            try {
                xmlIn = Assembly.GetAssembly(this.GetType()).GetManifestResourceStream(typeof(T).FullName + ".xml");
                StreamReader xmlInReader = new StreamReader(xmlIn);
                string xmlStr = xmlInReader.ReadToEnd();

                MwsXmlReader reader = new MwsXmlReader(xmlStr);
                T obj = (T) Activator.CreateInstance(typeof(T));
                obj.ReadFragmentFrom(reader);
                obj.ResponseHeaderMetadata = new ResponseHeaderMetadata("mockRequestId", "A,B,C", "mockTimestamp", 0d, 0d, new DateTime());
                return obj;
            }
            catch (Exception e)
            {
                throw MwsUtil.Wrap(e);
            }
            finally
            {
                if (xmlIn != null) { xmlIn.Close(); }
            }
        }
    }
}
