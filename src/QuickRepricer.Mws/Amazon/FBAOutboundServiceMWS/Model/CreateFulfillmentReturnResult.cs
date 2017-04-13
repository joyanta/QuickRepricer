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
 * Create Fulfillment Return Result
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
    public class CreateFulfillmentReturnResult : AbstractMwsObject
    {

        private ReturnItemList _returnItemList;
        private InvalidReturnItemList _invalidReturnItemList;
        private ReturnAuthorizationList _returnAuthorizationList;

        /// <summary>
        /// Gets and sets the ReturnItemList property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ReturnItemList")]
        public ReturnItemList ReturnItemList
        {
            get { return this._returnItemList; }
            set { this._returnItemList = value; }
        }

        /// <summary>
        /// Sets the ReturnItemList property.
        /// </summary>
        /// <param name="returnItemList">ReturnItemList property.</param>
        /// <returns>this instance.</returns>
        public CreateFulfillmentReturnResult WithReturnItemList(ReturnItemList returnItemList)
        {
            this._returnItemList = returnItemList;
            return this;
        }

        /// <summary>
        /// Checks if ReturnItemList property is set.
        /// </summary>
        /// <returns>true if ReturnItemList property is set.</returns>
        public bool IsSetReturnItemList()
        {
            return this._returnItemList != null;
        }

        /// <summary>
        /// Gets and sets the InvalidReturnItemList property.
        /// </summary>
        [XmlElementAttribute(ElementName = "InvalidReturnItemList")]
        public InvalidReturnItemList InvalidReturnItemList
        {
            get { return this._invalidReturnItemList; }
            set { this._invalidReturnItemList = value; }
        }

        /// <summary>
        /// Sets the InvalidReturnItemList property.
        /// </summary>
        /// <param name="invalidReturnItemList">InvalidReturnItemList property.</param>
        /// <returns>this instance.</returns>
        public CreateFulfillmentReturnResult WithInvalidReturnItemList(InvalidReturnItemList invalidReturnItemList)
        {
            this._invalidReturnItemList = invalidReturnItemList;
            return this;
        }

        /// <summary>
        /// Checks if InvalidReturnItemList property is set.
        /// </summary>
        /// <returns>true if InvalidReturnItemList property is set.</returns>
        public bool IsSetInvalidReturnItemList()
        {
            return this._invalidReturnItemList != null;
        }

        /// <summary>
        /// Gets and sets the ReturnAuthorizationList property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ReturnAuthorizationList")]
        public ReturnAuthorizationList ReturnAuthorizationList
        {
            get { return this._returnAuthorizationList; }
            set { this._returnAuthorizationList = value; }
        }

        /// <summary>
        /// Sets the ReturnAuthorizationList property.
        /// </summary>
        /// <param name="returnAuthorizationList">ReturnAuthorizationList property.</param>
        /// <returns>this instance.</returns>
        public CreateFulfillmentReturnResult WithReturnAuthorizationList(ReturnAuthorizationList returnAuthorizationList)
        {
            this._returnAuthorizationList = returnAuthorizationList;
            return this;
        }

        /// <summary>
        /// Checks if ReturnAuthorizationList property is set.
        /// </summary>
        /// <returns>true if ReturnAuthorizationList property is set.</returns>
        public bool IsSetReturnAuthorizationList()
        {
            return this._returnAuthorizationList != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _returnItemList = reader.Read<ReturnItemList>("ReturnItemList");
            _invalidReturnItemList = reader.Read<InvalidReturnItemList>("InvalidReturnItemList");
            _returnAuthorizationList = reader.Read<ReturnAuthorizationList>("ReturnAuthorizationList");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("ReturnItemList", _returnItemList);
            writer.Write("InvalidReturnItemList", _invalidReturnItemList);
            writer.Write("ReturnAuthorizationList", _returnAuthorizationList);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentOutboundShipment/2010-10-01/", "CreateFulfillmentReturnResult", this);
        }


        public CreateFulfillmentReturnResult() : base()
        {
        }
    }
}
