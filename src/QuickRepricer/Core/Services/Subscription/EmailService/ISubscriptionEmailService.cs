using Stripe;
using System.Threading.Tasks;

namespace QuickRepricer.Core.Services.Subscription.EmailService
{
    public interface ISubscriptionEmailService
    {
        Task SendRefundEmailAsync(StripeCharge stripeCharge);
        Task SendTrialEndEmailAsync(StripeSubscription subscription);
        Task SendSubscriptionPaymentReceiptEmailAsync(StripeInvoice invoice, StripeCustomer customer);
    }
}
