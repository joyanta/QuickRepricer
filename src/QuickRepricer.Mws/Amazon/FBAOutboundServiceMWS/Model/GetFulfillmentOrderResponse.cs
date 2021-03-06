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
 * Get Fulfillment Order Response
 * API Version: 2010-10-01
 * Library Version: 2016-10-19
 * Generated: Wed Oct 19 08:37:54 PDT 2016
 */


using System;
using System.Xml;
using System.Xml.Serialization;
using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;

namespace QuickRepricer.Mws.Amazon.FBAOutboundServiceMWS.Model
{
    [XmlTypeAttribute(Namespace = "http://mws.amazonaws.com/FulfillmentOutboundShipment/2010-10-01/")]
    [XmlRootAttribute(Namespace = "http://mws.amazonaws.com/FulfillmentOutboundShipment/2010-10-01/", IsNullable = false)]
    public class GetFulfillmentOrderResponse : AbstractMwsObject, IMWSResponse
    {

        private GetFulfillmentOrderResult _getFulfillmentOrderResult;
        private ResponseMetadata _responseMetadata;
        private ResponseHeaderMetadata _responseHeaderMetadata;

        /// <summary>
        /// Gets and sets the GetFulfillmentOrderResult property.
        /// </summary>
        [XmlElementAttribute(ElementName = "GetFulfillmentOrderResult")]
        public GetFulfillmentOrderResult GetFulfillmentOrderResult
        {
            get { return this._getFulfillmentOrderResult; }
            set { this._getFulfillmentOrderResult = value; }
        }

        /// <summary>
        /// Sets the GetFulfillmentOrderResult property.
        /// </summary>
        /// <param name="getFulfillmentOrderResult">GetFulfillmentOrderResult property.</param>
        /// <returns>this instance.</returns>
        public GetFulfillmentOrderResponse WithGetFulfillmentOrderResult(GetFulfillmentOrderResult getFulfillmentOrderResult)
        {
            this._getFulfillmentOrderResult = getFulfillmentOrderResult;
            return this;
        }

        /// <summary>
        /// Checks if GetFulfillmentOrderResult property is set.
        /// </summary>
        /// <returns>true if GetFulfillmentOrderResult property is set.</returns>
        public bool IsSetGetFulfillmentOrderResult()
        {
            return this._getFulfillmentOrderResult != null;
        }

        /// <summary>
        /// Gets and sets the ResponseMetadata property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ResponseMetadata")]
        public ResponseMetadata ResponseMetadata
        {
            get { return this._responseMetadata; }
            set { this._responseMetadata = value; }
        }

        /// <summary>
        /// Sets the ResponseMetadata property.
        /// </summary>
        /// <param name="responseMetadata">ResponseMetadata property.</param>
        /// <returns>this instance.</returns>
        public GetFulfillmentOrderResponse WithResponseMetadata(ResponseMetadata responseMetadata)
        {
            this._responseMetadata = responseMetadata;
            return this;
        }

        /// <summary>
        /// Checks if ResponseMetadata property is set.
        /// </summary>
        /// <returns>true if ResponseMetadata property is set.</returns>
        public bool IsSetResponseMetadata()
        {
            return this._responseMetadata != null;
        }

        /// <summary>
        /// Gets and sets the ResponseHeaderMetadata property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ResponseHeaderMetadata")]
        public ResponseHeaderMetadata ResponseHeaderMetadata
        {
            get { return this._responseHeaderMetadata; }
            set { this._responseHeaderMetadata = value; }
        }

        /// <summary>
        /// Sets the ResponseHeaderMetadata property.
        /// </summary>
        /// <param name="responseHeaderMetadata">ResponseHeaderMetadata property.</param>
        /// <returns>this instance.</returns>
        public GetFulfillmentOrderResponse WithResponseHeaderMetadata(ResponseHeaderMetadata responseHeaderMetadata)
        {
            this._responseHeaderMetadata = responseHeaderMetadata;
            return this;
        }

        /// <summary>
        /// Checks if ResponseHeaderMetadata property is set.
        /// </summary>
        /// <returns>true if ResponseHeaderMetadata property is set.</returns>
        public bool IsSetResponseHeaderMetadata()
        {
            return this._responseHeaderMetadata != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _getFulfillmentOrderResult = reader.Read<GetFulfillmentOrderResult>("GetFulfillmentOrderResult");
            _responseMetadata = reader.Read<ResponseMetadata>("ResponseMetadata");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("GetFulfillmentOrderResult", _getFulfillmentOrderResult);
            writer.Write("ResponseMetadata", _responseMetadata);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentOutboundShipment/2010-10-01/", "GetFulfillmentOrderResponse", this);
        }


        public GetFulfillmentOrderResponse() : base()
        {
        }
    }
}
