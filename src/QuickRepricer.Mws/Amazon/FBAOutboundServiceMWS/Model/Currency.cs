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
 * Currency
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
    public class Currency : AbstractMwsObject
    {

        private string _currencyCode;
        private string _value;

        /// <summary>
        /// Gets and sets the CurrencyCode property.
        /// </summary>
        [XmlElementAttribute(ElementName = "CurrencyCode")]
        public string CurrencyCode
        {
            get { return this._currencyCode; }
            set { this._currencyCode = value; }
        }

        /// <summary>
        /// Sets the CurrencyCode property.
        /// </summary>
        /// <param name="currencyCode">CurrencyCode property.</param>
        /// <returns>this instance.</returns>
        public Currency WithCurrencyCode(string currencyCode)
        {
            this._currencyCode = currencyCode;
            return this;
        }

        /// <summary>
        /// Checks if CurrencyCode property is set.
        /// </summary>
        /// <returns>true if CurrencyCode property is set.</returns>
        public bool IsSetCurrencyCode()
        {
            return this._currencyCode != null;
        }

        /// <summary>
        /// Gets and sets the Value property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Value")]
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        /// <summary>
        /// Sets the Value property.
        /// </summary>
        /// <param name="value">Value property.</param>
        /// <returns>this instance.</returns>
        public Currency WithValue(string value)
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


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _currencyCode = reader.Read<string>("CurrencyCode");
            _value = reader.Read<string>("Value");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("CurrencyCode", _currencyCode);
            writer.Write("Value", _value);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentOutboundShipment/2010-10-01/", "Currency", this);
        }

    public Currency (string currencyCode,string value) : base() {
        this._currencyCode = currencyCode;
        this._value = value;
    }

        public Currency() : base()
        {
        }
    }
}
