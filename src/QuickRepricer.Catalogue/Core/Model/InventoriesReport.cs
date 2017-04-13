using System.Collections.Generic;

namespace QuickRepricer.Catalogue.Core.Model
{
    public class InventoryReport
    {
        public Inventory Inventory { get; set; }

        public List<Item> Items { get; set; }
    }
}
