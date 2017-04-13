using System;
using System.Threading.Tasks;

namespace QuickRepricer.Integration.Services.Subscription
{
    public class SubscriptionService : ISubscriptionService
    {
        public Task CreateOrUpdateAsync(string userName, string merchantId)
        {
            return Task.FromResult(0);
        }

    }
}
