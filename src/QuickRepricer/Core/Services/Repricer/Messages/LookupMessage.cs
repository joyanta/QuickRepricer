namespace QuickRepricer.Core.Services.Repricer.Messages
{
    public class LookupMessage
    {
        public string ASIM { get; private set; }

        public LookupMessage(string asim)
        {
            ASIM = asim;
        }
    }
}
