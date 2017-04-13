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
 * Guidance Reason List
 * API Version: 2010-10-01
 * Library Version: 2016-10-05
 * Generated: Wed Oct 05 06:15:39 PDT 2016
 */


using System;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;
using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;

namespace QuickRepricer.Mws.Amazon.FBAInboundServiceMWS.Model
{
    [XmlTypeAttribute(Namespace = "http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/")]
    [XmlRootAttribute(Namespace = "http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/", IsNullable = false)]
    public class GuidanceReasonList : AbstractMwsObject
    {

        private List<string> _guidanceReason;

        /// <summary>
        /// Gets and sets the GuidanceReason property.
        /// </summary>
        [XmlElementAttribute(ElementName = "GuidanceReason")]
        public List<string> GuidanceReason
        {
            get
            {
                if(this._guidanceReason == null)
                {
                    this._guidanceReason = new List<string>();
                }
                return this._guidanceReason;
            }
            set { this._guidanceReason = value; }
        }

        /// <summary>
        /// Sets the GuidanceReason property.
        /// </summary>
        /// <param name="guidanceReason">GuidanceReason property.</param>
        /// <returns>this instance.</returns>
        public GuidanceReasonList WithGuidanceReason(string[] guidanceReason)
        {
            this._guidanceReason.AddRange(guidanceReason);
            return this;
        }

        /// <summary>
        /// Checks if GuidanceReason property is set.
        /// </summary>
        /// <returns>true if GuidanceReason property is set.</returns>
        public bool IsSetGuidanceReason()
        {
            return this.GuidanceReason.Count > 0;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _guidanceReason = reader.ReadList<string>("GuidanceReason");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.WriteList("GuidanceReason", _guidanceReason);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/", "GuidanceReasonList", this);
        }


        public GuidanceReasonList() : base()
        {
        }
    }
}
