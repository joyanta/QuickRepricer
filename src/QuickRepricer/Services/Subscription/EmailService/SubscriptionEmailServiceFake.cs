using System;
using System.Threading.Tasks;
using Stripe;
using QuickRepricer.Core.Services.Subscription.EmailService;

namespace QuickRepricer.Services.Subscription.EmailService
{
    public class SubscriptionEmailServiceFake : ISubscriptionEmailService
    {
        public Task SendRefundEmailAsync(StripeCharge stripeCharge)
        {
            throw new NotImplementedException();
        }

        public Task SendSubscriptionPaymentReceiptEmailAsync(StripeInvoice invoice, StripeCustomer customer)
        {
            throw new NotImplementedException();
        }

        public Task SendTrialEndEmailAsync(StripeSubscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}
