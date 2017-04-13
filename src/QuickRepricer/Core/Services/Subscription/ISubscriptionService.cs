using Stripe;
using QuickRepricer.Core.Models;
using System.Threading.Tasks;

namespace QuickRepricer.Core.Services.Subscription
{
    public interface ISubscriptionService
    {
        Task CreateOrUpdateAsync(string userName, Plan plan, string stripeToken);

        StripeSubscriptionService GetSubscriptionService();

        StripeCustomerService GetCustomerService();
    }
}
