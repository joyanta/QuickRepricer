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
 * Label
 * API Version: 2015-06-01
 * Library Version: 2016-03-30
 * Generated: Tue Mar 29 18:59:58 UTC 2016
 */


using System;
using System.Xml;
using QuickRepricer.Mws.Amazon.MWSClientCsRuntime;

namespace QuickRepricer.Mws.Amazon.MWSMerchantFulfillmentService.Model
{
    public class Label : AbstractMwsObject
    {

        private LabelDimensions _dimensions;
        private FileContents _fileContents;

        /// <summary>
        /// Gets and sets the Dimensions property.
        /// </summary>
        public LabelDimensions Dimensions
        {
            get { return this._dimensions; }
            set { this._dimensions = value; }
        }

        /// <summary>
        /// Sets the Dimensions property.
        /// </summary>
        /// <param name="dimensions">Dimensions property.</param>
        /// <returns>this instance.</returns>
        public Label WithDimensions(LabelDimensions dimensions)
        {
            this._dimensions = dimensions;
            return this;
        }

        /// <summary>
        /// Checks if Dimensions property is set.
        /// </summary>
        /// <returns>true if Dimensions property is set.</returns>
        public bool IsSetDimensions()
        {
            return this._dimensions != null;
        }

        /// <summary>
        /// Gets and sets the FileContents property.
        /// </summary>
        public FileContents FileContents
        {
            get { return this._fileContents; }
            set { this._fileContents = value; }
        }

        /// <summary>
        /// Sets the FileContents property.
        /// </summary>
        /// <param name="fileContents">FileContents property.</param>
        /// <returns>this instance.</returns>
        public Label WithFileContents(FileContents fileContents)
        {
            this._fileContents = fileContents;
            return this;
        }

        /// <summary>
        /// Checks if FileContents property is set.
        /// </summary>
        /// <returns>true if FileContents property is set.</returns>
        public bool IsSetFileContents()
        {
            return this._fileContents != null;
        }


        public override void ReadFragmentFrom(IMwsReader reader)
        {
            _dimensions = reader.Read<LabelDimensions>("Dimensions");
            _fileContents = reader.Read<FileContents>("FileContents");
        }

        public override void WriteFragmentTo(IMwsWriter writer)
        {
            writer.Write("Dimensions", _dimensions);
            writer.Write("FileContents", _fileContents);
        }

        public override void WriteTo(IMwsWriter writer)
        {
            writer.Write("https://mws.amazonservices.com/MerchantFulfillment/2015-06-01", "Label", this);
        }

        public Label() : base()
        {
        }
    }
}
