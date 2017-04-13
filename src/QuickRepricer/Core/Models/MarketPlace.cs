namespace QuickRepricer.Core.Models
{
    public class MarketPlace
    {
        public int Id { get; set; }

        public string MarketplaceId { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
