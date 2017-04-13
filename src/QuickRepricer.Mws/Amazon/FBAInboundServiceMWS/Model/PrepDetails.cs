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
 * Prep Details
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
    public class PrepDetails : AbstractMwsObject
    {

        private string _prepInstruction;
        private string _prepOwner;

        /// <summary>
        /// Gets and sets the PrepInstruction property.
        /// </summary>
        [XmlElementAttribute(ElementName = "PrepInstruction")]
        public string PrepInstruction
        {
            get { return this._prepInstruction; }
            set { this._prepInstruction = value; }
        }

        /// <summary>
        /// Sets the PrepInstruction property.
        /// </summary>
        /// <param name="prepInstruction">PrepInstruction property.</param>
        /// <returns>this instance.</returns>
        public PrepDetails WithPrepInstruction(string prepInstruction)
        {
            this._prepInstruction = prepInstruction;
            return this;
        }

        /// <summary>
        /// Checks if PrepInstruction property is set.
        /// </summary>
        /// <returns>true if PrepInstruction property is set.</returns>
        public bool IsSetPrepInstruction()
        {
            return this._prepInstruction != null;
        }

        /// <summary>
        /// Gets and sets the PrepOwner property.
        /// </summary>
        [XmlElementAttribute(ElementName = "PrepOwner")]
        public string PrepOwner
        {
            get { return this._prepOwner; }
            set { this._prepOwner = value; }
        }

        /// <summary>
        /// Sets the PrepOwner property.
        /// </summary>
        /// <param name="prepOwner">PrepOwner property.</param>
        /// <returns>this instance.</returns>
        public PrepDetails WithPrepOwner(string prepOwner)
        {
            this._prepOwner = prepOwner;
            return this;
        }

        /// <summary>
        /// Checks if PrepOwner property is set.
        /// </summary>
        /// <returns>true if PrepOwner property is set.</returns>
        public bool IsSetPrepOwner()
        {
            return this._prepOwner != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _prepInstruction = reader.Read<string>("PrepInstruction");
            _prepOwner = reader.Read<string>("PrepOwner");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("PrepInstruction", _prepInstruction);
            writer.Write("PrepOwner", _prepOwner);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/", "PrepDetails", this);
        }


        public PrepDetails() : base()
        {
        }
    }
}
