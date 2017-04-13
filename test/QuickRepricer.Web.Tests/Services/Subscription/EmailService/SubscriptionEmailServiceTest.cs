using FluentAssertions;
using Moq;
using QuickRepricer.Core.Services.Subscription.EmailService;
using Stripe;
using System.Threading.Tasks;
using Xunit;

namespace QuickRepricer.Web.Tests.Services.Subscription.EmailService
{
    public class SubscriptionEmailServiceTest
    {
        private Mock<ISubscriptionEmailService> _subscriptionEmailService;
        public SubscriptionEmailServiceTest()
        {
            _subscriptionEmailService = new Mock<ISubscriptionEmailService>();
        }

        [Fact]
        public void SendRefundEmail_Successfully()
        {
            // arrange
            var charge = new StripeCharge();

            _subscriptionEmailService.Setup(stub => stub.SendRefundEmailAsync(charge))
                .Returns(Task.FromResult(0));

            //action 
            var task = _subscriptionEmailService.Object.SendRefundEmailAsync(charge);

            //aseert
            task.Should().NotBeNull();
        }

        [Fact]
        public void SendTrialEndEmail_Successfully()
        {
            var subscription = new StripeSubscription();
            _subscriptionEmailService.Setup(stub => stub.SendTrialEndEmailAsync(subscription))
                .Returns(Task.FromResult(0));

            //action 
            var task = _subscriptionEmailService.Object.SendTrialEndEmailAsync(subscription);

            //aseert
            task.Should().NotBeNull();
        }


        [Fact]
        public void SendSubscriptionPaymentReceiptEmail_Successfully()
        {
       
            var invoice = new StripeInvoice();
            var customer = new StripeCustomer();

            _subscriptionEmailService.Setup(stub => stub.SendSubscriptionPaymentReceiptEmailAsync(invoice, customer))
                .Returns(Task.FromResult(0));

            //action 
            var task = _subscriptionEmailService.Object.SendSubscriptionPaymentReceiptEmailAsync(invoice, customer);

            //aseert
            task.Should().NotBeNull();
        }
     
    }
}
