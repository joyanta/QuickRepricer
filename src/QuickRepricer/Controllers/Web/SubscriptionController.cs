using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Threading.Tasks;
using QuickRepricer.Core.Services.Subscription;
using QuickRepricer.Core.Services.Subscription.Plans;
using QuickRepricer.Core.ViewModels.SubscriptionViewModels;

namespace QuickRepricer.Controllers.Web
{
    [RequireHttps]
    public class SubscriptionController : Controller
    {
        private IPlanService _planService;
        private ISubscriptionService _subscriptionService;
        private IConfigurationRoot _config;

        public SubscriptionController(IPlanService planService, 
            ISubscriptionService subscriptionService,
            IConfigurationRoot config)
        {

            _planService = planService;
            _subscriptionService = subscriptionService;
            _config = config;
        }
        
        public async Task<IActionResult> Index()
        {
            var plans = await _planService.ListAsync();
            var viewModel = new SubscriptionIndexViewModel()
            {
                Plans = plans
            };
            
            return View(viewModel);
        }


        public async Task<IActionResult> Billing(int planId)
        {
            string stripePublishableKey = _config["PaymentSettings:Stripe:PublicKey"];

            var plan = await _planService.FindAsync(planId);

            var viewModel = new BillingViewModel() {
                Plan = plan,
                StripePublishableKey = stripePublishableKey
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Billing(BillingViewModel billingViewModel)
        {
            billingViewModel.Plan = await _planService.FindAsync(billingViewModel.Plan.Id);

            try
            {
                await _subscriptionService.CreateOrUpdateAsync(User.Identity.Name, billingViewModel.Plan, billingViewModel.StripeToken);
            }
            catch (StripeException stripeException)
            {
                ModelState.AddModelError(string.Empty, stripeException.Message);
                return View(billingViewModel);
            }

            return RedirectToAction("Index", "Dashboard");
        }

    }
}
