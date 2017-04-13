using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using QuickRepricer.Controllers.Web;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.Services.Subscription;
using QuickRepricer.Core.Services.Subscription.Plans;
using QuickRepricer.Core.ViewModels.SubscriptionViewModels;
using QuickRepricer.Web.Tests.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QuickRepricer.Web.Tests.Controllers.Web
{
    public class SubscriptionControllerTests
    {
        private Mock<IConfigurationRoot> _config;
        private Mock<IPlanService> _planService;
        private Mock<ISubscriptionService> _subscriptionService;

        private SubscriptionController _subscriptionController;
        

        public SubscriptionControllerTests()
        {
            _planService = new Mock<IPlanService>();
            _config = new Mock<IConfigurationRoot>();
            _subscriptionService = new Mock<ISubscriptionService>();
          
            _subscriptionController = new SubscriptionController(_planService.Object, _subscriptionService.Object, _config.Object);
            _subscriptionController.MockCurrentUser("1", "user@gmail.com");
        }

        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Wire up
            var plans = new List<Plan>
            {
                new Plan()
            };
            _planService.Setup(stub => stub.ListAsync()).ReturnsAsync(plans);

            // Action
            var result = (await _subscriptionController.Index());

            // Verify
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task Billing_ReturnsAViewResult()
        {
            // Wireup
            _config.Setup(stub => stub["PaymentSettings:Stripe:PublicKey"])
                .Returns("StripeKey");

            var plan = new Plan()
            {
                Id = 1
            };

            _planService.Setup(stub => stub.FindAsync(plan.Id)).ReturnsAsync(plan);

            // action
            var result = await _subscriptionController.Billing(plan.Id);

            // verify
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        public async Task BillingPost_ReturnsRedirectToDashBoardIndex()
        {
            //wire up
            var plan = new Plan()
            {
                Id = 1
            };

            var billingViewModel = new BillingViewModel()
            {
                 Plan = plan,
                 StripeToken = ""
            };

            _planService.Setup(stub => stub.FindAsync(billingViewModel.Plan.Id)).ReturnsAsync(plan);

            _subscriptionService.Setup(stub => stub.CreateOrUpdateAsync(_subscriptionController.User.Identity.Name,
                billingViewModel.Plan, billingViewModel.StripeToken)).Returns(Task.FromResult(0));

            // Action
            var result = await _subscriptionController.Billing(billingViewModel);

            //verify
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();

            result.Should().BeOfType<RedirectToActionResult>()
              .Subject.ControllerName.Should().Be("Dashboard");

            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().Be("Index");
        }
    }
}
