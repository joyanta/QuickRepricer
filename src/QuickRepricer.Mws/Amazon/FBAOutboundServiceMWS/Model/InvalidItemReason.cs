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
 * Invalid Item Reason
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
    public class InvalidItemReason : AbstractMwsObject
    {

        private string _invalidItemReasonCode;
        private string _description;

        /// <summary>
        /// Gets and sets the InvalidItemReasonCode property.
        /// </summary>
        [XmlElementAttribute(ElementName = "InvalidItemReasonCode")]
        public string InvalidItemReasonCode
        {
            get { return this._invalidItemReasonCode; }
            set { this._invalidItemReasonCode = value; }
        }

        /// <summary>
        /// Sets the InvalidItemReasonCode property.
        /// </summary>
        /// <param name="invalidItemReasonCode">InvalidItemReasonCode property.</param>
        /// <returns>this instance.</returns>
        public InvalidItemReason WithInvalidItemReasonCode(string invalidItemReasonCode)
        {
            this._invalidItemReasonCode = invalidItemReasonCode;
            return this;
        }

        /// <summary>
        /// Checks if InvalidItemReasonCode property is set.
        /// </summary>
        /// <returns>true if InvalidItemReasonCode property is set.</returns>
        public bool IsSetInvalidItemReasonCode()
        {
            return this._invalidItemReasonCode != null;
        }

        /// <summary>
        /// Gets and sets the Description property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Description")]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        /// Sets the Description property.
        /// </summary>
        /// <param name="description">Description property.</param>
        /// <returns>this instance.</returns>
        public InvalidItemReason WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        /// <summary>
        /// Checks if Description property is set.
        /// </summary>
        /// <returns>true if Description property is set.</returns>
        public bool IsSetDescription()
        {
            return this._description != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _invalidItemReasonCode = reader.Read<string>("InvalidItemReasonCode");
            _description = reader.Read<string>("Description");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("InvalidItemReasonCode", _invalidItemReasonCode);
            writer.Write("Description", _description);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentOutboundShipment/2010-10-01/", "InvalidItemReason", this);
        }

    public InvalidItemReason (string invalidItemReasonCode,string description) : base() {
        this._invalidItemReasonCode = invalidItemReasonCode;
        this._description = description;
    }

        public InvalidItemReason() : base()
        {
        }
    }
}
