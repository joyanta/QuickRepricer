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
 * Get Service Status Request
 * API Version: 2010-10-01
 * Library Version: 2014-09-30
 * Generated: Mon Mar 21 09:01:30 PDT 2016
 */
 
using System.Xml.Serialization;
using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;

namespace FBAInventoryServiceMWS.Model
{
    [XmlTypeAttribute(Namespace = "http://mws.amazonaws.com/FulfillmentInventory/2010-10-01/")]
    [XmlRootAttribute(Namespace = "http://mws.amazonaws.com/FulfillmentInventory/2010-10-01/", IsNullable = false)]
    public class GetServiceStatusRequest : AbstractMwsObject
    {

        private string _sellerId;
        private string _mwsAuthToken;
        private string _marketplace;

        /// <summary>
        /// Gets and sets the SellerId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "SellerId")]
        public string SellerId
        {
            get { return this._sellerId; }
            set { this._sellerId = value; }
        }

        /// <summary>
        /// Sets the SellerId property.
        /// </summary>
        /// <param name="sellerId">SellerId property.</param>
        /// <returns>this instance.</returns>
        public GetServiceStatusRequest WithSellerId(string sellerId)
        {
            this._sellerId = sellerId;
            return this;
        }

        /// <summary>
        /// Checks if SellerId property is set.
        /// </summary>
        /// <returns>true if SellerId property is set.</returns>
        public bool IsSetSellerId()
        {
            return this._sellerId != null;
        }

        /// <summary>
        /// Gets and sets the MWSAuthToken property.
        /// </summary>
        [XmlElementAttribute(ElementName = "MWSAuthToken")]
        public string MWSAuthToken
        {
            get { return this._mwsAuthToken; }
            set { this._mwsAuthToken = value; }
        }

        /// <summary>
        /// Sets the MWSAuthToken property.
        /// </summary>
        /// <param name="mwsAuthToken">MWSAuthToken property.</param>
        /// <returns>this instance.</returns>
        public GetServiceStatusRequest WithMWSAuthToken(string mwsAuthToken)
        {
            this._mwsAuthToken = mwsAuthToken;
            return this;
        }

        /// <summary>
        /// Checks if MWSAuthToken property is set.
        /// </summary>
        /// <returns>true if MWSAuthToken property is set.</returns>
        public bool IsSetMWSAuthToken()
        {
            return this._mwsAuthToken != null;
        }

        /// <summary>
        /// Gets and sets the Marketplace property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Marketplace")]
        public string Marketplace
        {
            get { return this._marketplace; }
            set { this._marketplace = value; }
        }

        /// <summary>
        /// Sets the Marketplace property.
        /// </summary>
        /// <param name="marketplace">Marketplace property.</param>
        /// <returns>this instance.</returns>
        public GetServiceStatusRequest WithMarketplace(string marketplace)
        {
            this._marketplace = marketplace;
            return this;
        }

        /// <summary>
        /// Checks if Marketplace property is set.
        /// </summary>
        /// <returns>true if Marketplace property is set.</returns>
        public bool IsSetMarketplace()
        {
            return this._marketplace != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _sellerId = reader.Read<string>("SellerId");
            _mwsAuthToken = reader.Read<string>("MWSAuthToken");
            _marketplace = reader.Read<string>("Marketplace");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("SellerId", _sellerId);
            writer.Write("MWSAuthToken", _mwsAuthToken);
            writer.Write("Marketplace", _marketplace);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentInventory/2010-10-01/", "GetServiceStatusRequest", this);
        }

        public GetServiceStatusRequest() : base()
        {
        }
    }
}
