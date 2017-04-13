namespace QuickRepricer.Core.Services.Repricer
{
    public interface IRepricerService<S>
        where S : class
    {
        void Start(); 

        void Stop();
           
    }
}
