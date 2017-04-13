using System;
using System.Threading.Tasks;
using Stripe;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.Services.Subscription;

namespace QuickRepricer.Services.Subscription
{
    public class SubscriptionServiceFake : ISubscriptionService
    {
        public Task CreateOrUpdateAsync(string userName, Plan plan, string stripeToken)
        {
            throw new NotImplementedException();
        }

        public StripeCustomerService GetCustomerService()
        {
            throw new NotImplementedException();
        }

        public StripeSubscriptionService GetSubscriptionService()
        {
            throw new NotImplementedException();
        }
    }
}
