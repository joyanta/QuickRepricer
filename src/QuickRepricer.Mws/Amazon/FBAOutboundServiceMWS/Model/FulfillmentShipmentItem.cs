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
 * Fulfillment Shipment Item
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
    public class FulfillmentShipmentItem : AbstractMwsObject
    {

        private string _sellerSKU;
        private string _sellerFulfillmentOrderItemId;
        private decimal _quantity;
        private decimal? _packageNumber;

        /// <summary>
        /// Gets and sets the SellerSKU property.
        /// </summary>
        [XmlElementAttribute(ElementName = "SellerSKU")]
        public string SellerSKU
        {
            get { return this._sellerSKU; }
            set { this._sellerSKU = value; }
        }

        /// <summary>
        /// Sets the SellerSKU property.
        /// </summary>
        /// <param name="sellerSKU">SellerSKU property.</param>
        /// <returns>this instance.</returns>
        public FulfillmentShipmentItem WithSellerSKU(string sellerSKU)
        {
            this._sellerSKU = sellerSKU;
            return this;
        }

        /// <summary>
        /// Checks if SellerSKU property is set.
        /// </summary>
        /// <returns>true if SellerSKU property is set.</returns>
        public bool IsSetSellerSKU()
        {
            return this._sellerSKU != null;
        }

        /// <summary>
        /// Gets and sets the SellerFulfillmentOrderItemId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "SellerFulfillmentOrderItemId")]
        public string SellerFulfillmentOrderItemId
        {
            get { return this._sellerFulfillmentOrderItemId; }
            set { this._sellerFulfillmentOrderItemId = value; }
        }

        /// <summary>
        /// Sets the SellerFulfillmentOrderItemId property.
        /// </summary>
        /// <param name="sellerFulfillmentOrderItemId">SellerFulfillmentOrderItemId property.</param>
        /// <returns>this instance.</returns>
        public FulfillmentShipmentItem WithSellerFulfillmentOrderItemId(string sellerFulfillmentOrderItemId)
        {
            this._sellerFulfillmentOrderItemId = sellerFulfillmentOrderItemId;
            return this;
        }

        /// <summary>
        /// Checks if SellerFulfillmentOrderItemId property is set.
        /// </summary>
        /// <returns>true if SellerFulfillmentOrderItemId property is set.</returns>
        public bool IsSetSellerFulfillmentOrderItemId()
        {
            return this._sellerFulfillmentOrderItemId != null;
        }

        /// <summary>
        /// Gets and sets the Quantity property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Quantity")]
        public decimal Quantity
        {
            get { return this._quantity; }
            set { this._quantity = value; }
        }

        /// <summary>
        /// Sets the Quantity property.
        /// </summary>
        /// <param name="quantity">Quantity property.</param>
        /// <returns>this instance.</returns>
        public FulfillmentShipmentItem WithQuantity(decimal quantity)
        {
            this._quantity = quantity;
            return this;
        }

        /// <summary>
        /// Checks if Quantity property is set.
        /// </summary>
        /// <returns>true if Quantity property is set.</returns>
        public bool IsSetQuantity()
        {
            return this._quantity != null;
        }

        /// <summary>
        /// Gets and sets the PackageNumber property.
        /// </summary>
        [XmlElementAttribute(ElementName = "PackageNumber")]
        public decimal PackageNumber
        {
            get { return this._packageNumber.GetValueOrDefault(); }
            set { this._packageNumber = value; }
        }

        /// <summary>
        /// Sets the PackageNumber property.
        /// </summary>
        /// <param name="packageNumber">PackageNumber property.</param>
        /// <returns>this instance.</returns>
        public FulfillmentShipmentItem WithPackageNumber(decimal packageNumber)
        {
            this._packageNumber = packageNumber;
            return this;
        }

        /// <summary>
        /// Checks if PackageNumber property is set.
        /// </summary>
        /// <returns>true if PackageNumber property is set.</returns>
        public bool IsSetPackageNumber()
        {
            return this._packageNumber != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _sellerSKU = reader.Read<string>("SellerSKU");
            _sellerFulfillmentOrderItemId = reader.Read<string>("SellerFulfillmentOrderItemId");
            _quantity = reader.Read<decimal>("Quantity");
            _packageNumber = reader.Read<decimal?>("PackageNumber");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("SellerSKU", _sellerSKU);
            writer.Write("SellerFulfillmentOrderItemId", _sellerFulfillmentOrderItemId);
            writer.Write("Quantity", _quantity);
            writer.Write("PackageNumber", _packageNumber);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("http://mws.amazonaws.com/FulfillmentOutboundShipment/2010-10-01/", "FulfillmentShipmentItem", this);
        }

    public FulfillmentShipmentItem (string sellerSKU,string sellerFulfillmentOrderItemId,decimal quantity) : base() {
        this._sellerSKU = sellerSKU;
        this._sellerFulfillmentOrderItemId = sellerFulfillmentOrderItemId;
        this._quantity = quantity;
    }

        public FulfillmentShipmentItem() : base()
        {
        }
    }
}
