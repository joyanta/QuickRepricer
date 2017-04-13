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
 * Get Shipment Response
 * API Version: 2015-06-01
 * Library Version: 2016-03-30
 * Generated: Tue Mar 29 18:59:58 UTC 2016
 */


using System;
using System.Xml;
using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;

namespace QuickRepricer.Mws.Amazon.MWSMerchantFulfillmentService.Model
{
    public class GetShipmentResponse : AbstractMwsObject, IMWSResponse
    {

        private GetShipmentResult _getShipmentResult;
        private ResponseMetadata _responseMetadata;
        private ResponseHeaderMetadata _responseHeaderMetadata;

        /// <summary>
        /// Gets and sets the GetShipmentResult property.
        /// </summary>
        public GetShipmentResult GetShipmentResult
        {
            get { return this._getShipmentResult; }
            set { this._getShipmentResult = value; }
        }

        /// <summary>
        /// Sets the GetShipmentResult property.
        /// </summary>
        /// <param name="getShipmentResult">GetShipmentResult property.</param>
        /// <returns>this instance.</returns>
        public GetShipmentResponse WithGetShipmentResult(GetShipmentResult getShipmentResult)
        {
            this._getShipmentResult = getShipmentResult;
            return this;
        }

        /// <summary>
        /// Checks if GetShipmentResult property is set.
        /// </summary>
        /// <returns>true if GetShipmentResult property is set.</returns>
        public bool IsSetGetShipmentResult()
        {
            return this._getShipmentResult != null;
        }

        /// <summary>
        /// Gets and sets the ResponseMetadata property.
        /// </summary>
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
        public GetShipmentResponse WithResponseMetadata(ResponseMetadata responseMetadata)
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
        public GetShipmentResponse WithResponseHeaderMetadata(ResponseHeaderMetadata responseHeaderMetadata)
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
            _getShipmentResult = reader.Read<GetShipmentResult>("GetShipmentResult");
            _responseMetadata = reader.Read<ResponseMetadata>("ResponseMetadata");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("GetShipmentResult", _getShipmentResult);
            writer.Write("ResponseMetadata", _responseMetadata);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("https://mws.amazonservices.com/MerchantFulfillment/2015-06-01", "GetShipmentResponse", this);
        }

        public GetShipmentResponse() : base()
        {
        }
    }
}
