using QuickRepricer.Core.Models;

namespace QuickRepricer.Core.ViewModels.SubscriptionViewModels
{
    public class BillingViewModel
    {
        public Plan Plan { get; set; }
        public string StripePublishableKey { get; set; }
        public string StripeToken { get; set; }
    }
}
