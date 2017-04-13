namespace QuickRepricer.Core.Services.Repricer.Messages
{
    public class RepricedData
    {
        public string ASIM { get; private set; }
        public double Price { get; private set; }
     
        public RepricedData(string asim, double price)
        {
            ASIM = asim;
         
            Price = price;

        }
    }
}
