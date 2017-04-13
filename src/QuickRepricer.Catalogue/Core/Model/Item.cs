namespace QuickRepricer.Catalogue.Core.Model
{
	public class Item
	{
		public string ItemName { get; private set; }
		public string ListingId { get; private set; }
		public string SellerSku { get; private set; }
		public double Price { get; private set; }
		public int Quantity { get; private set; }
		public string ItemCondition { get; private set; }
		public string Asin { get; private set; }

		public Item(string itemName, string listingId, string sellerSku, 
			double price, int quantity, string itemCondition, string asin)
		{
			ItemName = itemName;
			ListingId = listingId;
			SellerSku = sellerSku;
			Price = price;
			Quantity = quantity;
			ItemCondition = itemCondition;
			Asin = asin;
		}
	}
}
