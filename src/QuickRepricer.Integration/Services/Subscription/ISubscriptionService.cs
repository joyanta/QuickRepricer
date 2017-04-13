using System.Threading.Tasks;

namespace QuickRepricer.Integration.Services.Subscription
{
    public interface ISubscriptionService
    {
        Task CreateOrUpdateAsync(string userName, string merchantId);
    }
}
