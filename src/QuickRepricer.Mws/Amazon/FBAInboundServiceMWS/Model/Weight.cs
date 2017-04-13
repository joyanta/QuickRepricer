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
 * Weight
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
    public class Weight : AbstractMwsObject
    {

        private decimal _value;
        private string _unit;

        /// <summary>
        /// Gets and sets the Value property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Value")]
        public decimal Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        /// <summary>
        /// Sets the Value property.
        /// </summary>
        /// <param name="value">Value property.</param>
        /// <returns>this instance.</returns>
        public Weight WithValue(decimal value)
        {
            this._value = value;
            return this;
        }

        /// <summary>
        /// Checks if Value property is set.
        /// </summary>
        /// <returns>true if Value property is set.</returns>
        public bool IsSetValue()
        {
            return this._value != null;
        }

        /// <summary>
        /// Gets and sets the Unit property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Unit")]
        public string Unit
        {
            get { return this._unit; }
            set { this._unit = value; }
        }

        /// <summary>
        /// Sets the Unit property.
        /// </summary>
        /// <param name="unit">Unit property.</param>
        /// <returns>this instance.</returns>
        public Weight WithUnit(string unit)
        {
            this._unit = unit;
            return this;
        }

        /// <summary>
        /// Checks if Unit property is set.
        /// </summary>
        /// <returns>true if Unit property is set.</returns>
        public bool IsSetUnit()
        {
            return this._unit != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _value = reader.Read<decimal>("Value");
            _unit = reader.Read<string>("Unit");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("Value", _value);
            writer.Write("Unit", _unit);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentInboundShipment/2010-10-01/", "Weight", this);
        }

    public Weight (decimal value,string unit) : base() {
        this._value = value;
        this._unit = unit;
    }

        public Weight() : base()
        {
        }
    }
}
