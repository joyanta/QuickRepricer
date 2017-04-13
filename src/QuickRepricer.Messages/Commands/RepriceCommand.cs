namespace QuickRepricer.Messages.Commands
{
    public class RepriceCommand
    {
        public string ASIM { get; private set; }
        public string MerchantSku { get; private set; }
        public double Price { get; private set; }

        public RepriceCommand(string asim, string merchantSku, double price)
        {
            ASIM = asim;
            MerchantSku = merchantSku;
            Price = price;
        }
    }
}
