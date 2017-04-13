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
 * Shipping Service Options
 * API Version: 2015-06-01
 * Library Version: 2016-03-30
 * Generated: Tue Mar 29 18:59:58 UTC 2016
 */


using System;
using System.Xml;
using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;

namespace QuickRepricer.Mws.Amazon.MWSMerchantFulfillmentService.Model
{
    public class ShippingServiceOptions : AbstractMwsObject
    {

        private string _deliveryExperience;
        private CurrencyAmount _declaredValue;
        private bool _carrierWillPickUp;

        /// <summary>
        /// Gets and sets the DeliveryExperience property.
        /// </summary>
        public string DeliveryExperience
        {
            get { return this._deliveryExperience; }
            set { this._deliveryExperience = value; }
        }

        /// <summary>
        /// Sets the DeliveryExperience property.
        /// </summary>
        /// <param name="deliveryExperience">DeliveryExperience property.</param>
        /// <returns>this instance.</returns>
        public ShippingServiceOptions WithDeliveryExperience(string deliveryExperience)
        {
            this._deliveryExperience = deliveryExperience;
            return this;
        }

        /// <summary>
        /// Checks if DeliveryExperience property is set.
        /// </summary>
        /// <returns>true if DeliveryExperience property is set.</returns>
        public bool IsSetDeliveryExperience()
        {
            return this._deliveryExperience != null;
        }

        /// <summary>
        /// Gets and sets the DeclaredValue property.
        /// </summary>
        public CurrencyAmount DeclaredValue
        {
            get { return this._declaredValue; }
            set { this._declaredValue = value; }
        }

        /// <summary>
        /// Sets the DeclaredValue property.
        /// </summary>
        /// <param name="declaredValue">DeclaredValue property.</param>
        /// <returns>this instance.</returns>
        public ShippingServiceOptions WithDeclaredValue(CurrencyAmount declaredValue)
        {
            this._declaredValue = declaredValue;
            return this;
        }

        /// <summary>
        /// Checks if DeclaredValue property is set.
        /// </summary>
        /// <returns>true if DeclaredValue property is set.</returns>
        public bool IsSetDeclaredValue()
        {
            return this._declaredValue != null;
        }

        /// <summary>
        /// Gets and sets the CarrierWillPickUp property.
        /// </summary>
        public bool CarrierWillPickUp
        {
            get { return this._carrierWillPickUp; }
            set { this._carrierWillPickUp = value; }
        }

        /// <summary>
        /// Sets the CarrierWillPickUp property.
        /// </summary>
        /// <param name="carrierWillPickUp">CarrierWillPickUp property.</param>
        /// <returns>this instance.</returns>
        public ShippingServiceOptions WithCarrierWillPickUp(bool carrierWillPickUp)
        {
            this._carrierWillPickUp = carrierWillPickUp;
            return this;
        }

        /// <summary>
        /// Checks if CarrierWillPickUp property is set.
        /// </summary>
        /// <returns>true if CarrierWillPickUp property is set.</returns>
        public bool IsSetCarrierWillPickUp()
        {
            return this._carrierWillPickUp != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _deliveryExperience = reader.Read<string>("DeliveryExperience");
            _declaredValue = reader.Read<CurrencyAmount>("DeclaredValue");
            _carrierWillPickUp = reader.Read<bool>("CarrierWillPickUp");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("DeliveryExperience", _deliveryExperience);
            writer.Write("DeclaredValue", _declaredValue);
            writer.Write("CarrierWillPickUp", _carrierWillPickUp);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("https://mws.amazonservices.com/MerchantFulfillment/2015-06-01", "ShippingServiceOptions", this);
        }

        public ShippingServiceOptions() : base()
        {
        }
    }
}
