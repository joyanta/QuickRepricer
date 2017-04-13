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
 * List Inbound Shipments By Next Token Response
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
    public class ListInboundShipmentsByNextTokenResponse : AbstractMwsObject, IMWSResponse
    {

        private ListInboundShipmentsByNextTokenResult _listInboundShipmentsByNextTokenResult;
        private ResponseMetadata _responseMetadata;
        private ResponseHeaderMetadata _responseHeaderMetadata;

        /// <summary>
        /// Gets and sets the ListInboundShipmentsByNextTokenResult property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ListInboundShipmentsByNextTokenResult")]
        public ListInboundShipmentsByNextTokenResult ListInboundShipmentsByNextTokenResult
        {
            get { return this._listInboundShipmentsByNextTokenResult; }
            set { this._listInboundShipmentsByNextTokenResult = value; }
        }

        /// <summary>
        /// Sets the ListInboundShipmentsByNextTokenResult property.
        /// </summary>
        /// <param name="listInboundShipmentsByNextTokenResult">ListInboundShipmentsByNextTokenResult property.</param>
        /// <returns>this instance.</returns>
        public ListInboundShipmentsByNextTokenResponse WithListInboundShipmentsByNextTokenResult(ListInboundShipmentsByNextTokenResult listInboundShipmentsByNextTokenResult)
        {
            this._listInboundShipmentsByNextTokenResult = listInboundShipmentsByNextTokenResult;
            return this;
        }

        /// <summary>
        /// Checks if ListInboundShipmentsByNextTokenResult property is set.
        /// </summary>
        /// <returns>true if ListInboundShipmentsByNextTokenResult property is set.</returns>
        public bool IsSetListInboundShipmentsByNextTokenResult()
        {
            return this._listInboundShipmentsByNextTokenResult != null;
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
        public ListInboundShipmentsByNextTokenResponse WithResponseMetadata(ResponseMetadata responseMetadata)
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
        public ListInboundShipmentsByNextTokenResponse WithResponseHeaderMetadata(ResponseHeaderMetadata responseHeaderMetadata)
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
            _listInboundShipmentsByNextTokenResult = reader.Read<ListInboundShipmentsByNextTokenResult>("ListInboundShipmentsByNextTokenResult");
            _responseMetadata = reader.Read<ResponseMetadata>("ResponseMetadata");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("ListInboundShipmentsByNextTokenResult", _listInboundShipmentsByNextTokenResult);
            writer.Write("ResponseMetadata", _responseMetadata);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/", "ListInboundShipmentsByNextTokenResponse", this);
        }


        public ListInboundShipmentsByNextTokenResponse() : base()
        {
        }
    }
}
