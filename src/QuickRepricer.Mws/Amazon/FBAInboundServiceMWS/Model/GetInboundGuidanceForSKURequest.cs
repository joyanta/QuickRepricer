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
 * Get Inbound Guidance For SKU Request
 * API Version: 2010-10-01
 * Library Version: 2016-10-05
 * Generated: Wed Oct 05 06:15:39 PDT 2016
 */


using System;
using System.Xml;
using System.Xml.Serialization;
using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;

namespace QuickRepricer.Mws.Amazon.FBAInboundServiceMWS.Model
{
    [XmlTypeAttribute(Namespace = "http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/")]
    [XmlRootAttribute(Namespace = "http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/", IsNullable = false)]
    public class GetInboundGuidanceForSKURequest : AbstractMwsObject
    {

        private string _sellerId;
        private string _mwsAuthToken;
        private SellerSKUList _sellerSKUList;
        private string _marketplaceId;

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
        public GetInboundGuidanceForSKURequest WithSellerId(string sellerId)
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
        public GetInboundGuidanceForSKURequest WithMWSAuthToken(string mwsAuthToken)
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
        /// Gets and sets the SellerSKUList property.
        /// </summary>
        [XmlElementAttribute(ElementName = "SellerSKUList")]
        public SellerSKUList SellerSKUList
        {
            get { return this._sellerSKUList; }
            set { this._sellerSKUList = value; }
        }

        /// <summary>
        /// Sets the SellerSKUList property.
        /// </summary>
        /// <param name="sellerSKUList">SellerSKUList property.</param>
        /// <returns>this instance.</returns>
        public GetInboundGuidanceForSKURequest WithSellerSKUList(SellerSKUList sellerSKUList)
        {
            this._sellerSKUList = sellerSKUList;
            return this;
        }

        /// <summary>
        /// Checks if SellerSKUList property is set.
        /// </summary>
        /// <returns>true if SellerSKUList property is set.</returns>
        public bool IsSetSellerSKUList()
        {
            return this._sellerSKUList != null;
        }

        /// <summary>
        /// Gets and sets the MarketplaceId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "MarketplaceId")]
        public string MarketplaceId
        {
            get { return this._marketplaceId; }
            set { this._marketplaceId = value; }
        }

        /// <summary>
        /// Sets the MarketplaceId property.
        /// </summary>
        /// <param name="marketplaceId">MarketplaceId property.</param>
        /// <returns>this instance.</returns>
        public GetInboundGuidanceForSKURequest WithMarketplaceId(string marketplaceId)
        {
            this._marketplaceId = marketplaceId;
            return this;
        }

        /// <summary>
        /// Checks if MarketplaceId property is set.
        /// </summary>
        /// <returns>true if MarketplaceId property is set.</returns>
        public bool IsSetMarketplaceId()
        {
            return this._marketplaceId != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _sellerId = reader.Read<string>("SellerId");
            _mwsAuthToken = reader.Read<string>("MWSAuthToken");
            _sellerSKUList = reader.Read<SellerSKUList>("SellerSKUList");
            _marketplaceId = reader.Read<string>("MarketplaceId");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("SellerId", _sellerId);
            writer.Write("MWSAuthToken", _mwsAuthToken);
            writer.Write("SellerSKUList", _sellerSKUList);
            writer.Write("MarketplaceId", _marketplaceId);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/", "GetInboundGuidanceForSKURequest", this);
        }

    public GetInboundGuidanceForSKURequest (string sellerId,SellerSKUList sellerSKUList,string marketplaceId) : base() {
        this._sellerId = sellerId;
        this._sellerSKUList = sellerSKUList;
        this._marketplaceId = marketplaceId;
    }

        public GetInboundGuidanceForSKURequest() : base()
        {
        }
    }
}
