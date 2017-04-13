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
 * Get My Price For ASIN Response
 * API Version: 2011-10-01
 * Library Version: 2016-06-01
 * Generated: Mon Jun 13 10:07:51 PDT 2016
 */

using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuickRepricer.Mws.Amazon.MarketplaceWebServiceProducts.Model
{
    [XmlTypeAttribute(Namespace = "http://mws.amazonservices.com/schema/Products/2011-10-01")]
    [XmlRootAttribute(Namespace = "http://mws.amazonservices.com/schema/Products/2011-10-01", IsNullable = false)]
    public class GetMyPriceForASINResponse : AbstractMwsObject, IMWSResponse
    {

        private List<GetMyPriceForASINResult> _getMyPriceForASINResult;
        private ResponseMetadata _responseMetadata;
        private ResponseHeaderMetadata _responseHeaderMetadata;

        /// <summary>
        /// Gets and sets the GetMyPriceForASINResult property.
        /// </summary>
        [XmlElementAttribute(ElementName = "GetMyPriceForASINResult")]
        public List<GetMyPriceForASINResult> GetMyPriceForASINResult
        {
            get
            {
                if(this._getMyPriceForASINResult == null)
                {
                    this._getMyPriceForASINResult = new List<GetMyPriceForASINResult>();
                }
                return this._getMyPriceForASINResult;
            }
            set { this._getMyPriceForASINResult = value; }
        }

        /// <summary>
        /// Sets the GetMyPriceForASINResult property.
        /// </summary>
        /// <param name="getMyPriceForASINResult">GetMyPriceForASINResult property.</param>
        /// <returns>this instance.</returns>
        public GetMyPriceForASINResponse WithGetMyPriceForASINResult(GetMyPriceForASINResult[] getMyPriceForASINResult)
        {
            this._getMyPriceForASINResult.AddRange(getMyPriceForASINResult);
            return this;
        }

        /// <summary>
        /// Checks if GetMyPriceForASINResult property is set.
        /// </summary>
        /// <returns>true if GetMyPriceForASINResult property is set.</returns>
        public bool IsSetGetMyPriceForASINResult()
        {
            return this.GetMyPriceForASINResult.Count > 0;
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
        public GetMyPriceForASINResponse WithResponseMetadata(ResponseMetadata responseMetadata)
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
        public GetMyPriceForASINResponse WithResponseHeaderMetadata(ResponseHeaderMetadata responseHeaderMetadata)
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
            _getMyPriceForASINResult = reader.ReadList<GetMyPriceForASINResult>("GetMyPriceForASINResult");
            _responseMetadata = reader.Read<ResponseMetadata>("ResponseMetadata");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.WriteList("GetMyPriceForASINResult", _getMyPriceForASINResult);
            writer.Write("ResponseMetadata", _responseMetadata);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonservices.com/schema/Products/2011-10-01", "GetMyPriceForASINResponse", this);
        }

        public GetMyPriceForASINResponse() : base()
        {
        }
    }
}
