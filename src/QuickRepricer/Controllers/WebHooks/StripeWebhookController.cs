using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.Services.Subscription;
using QuickRepricer.Core.Services.Subscription.EmailService;
using Stripe;
using System;
using System.Threading.Tasks;

namespace QuickRepricer.Controllers.WebHooks
{
    [RequireHttps]
    public class StripeWebhookController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ISubscriptionEmailService _emailService;
        private ISubscriptionService _subscriptionService;

        public StripeWebhookController(UserManager<ApplicationUser> userManager,
            ISubscriptionEmailService emailService, 
            ISubscriptionService subscriptionService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _subscriptionService = subscriptionService;
        }


        [HttpPost]
        public async Task<IActionResult> Index([FromBody] StripeEvent stripeEv)
        {

            StripeEvent stripeEvent = stripeEv;
            try
            {
                //stripeEvent = StripeEventUtility.ParseEvent(json);
                stripeEvent = await VerifyEventSentFromStripeAsync(stripeEvent);
                if (HasEventBeenProcessedPreviously(stripeEvent))
                {
                    return Ok();
                };
            }
            catch (Exception ex)
            {
                return BadRequest($"Unable to parse incoming event. The following error occurred: {ex.Message}");
            }

            if (stripeEvent == null)
            {
                return BadRequest("Incoming event empty");
            }


            var message = string.Empty;
            
            switch (stripeEvent.Type)
            {
                case StripeEvents.ChargeRefunded:
                    {
                        var charge = Mapper<StripeCharge>.MapFromJson(stripeEvent.Data.Object.ToString());
                        await _emailService.SendRefundEmailAsync(charge);
                        message = "ChargeRefunded";
                        break;
                    }
                case StripeEvents.CustomerSubscriptionTrialWillEnd:
                    {
                        var subscription = Mapper<StripeSubscription>.MapFromJson(stripeEvent.Data.Object.ToString());
                        await _emailService.SendTrialEndEmailAsync(subscription);

                        message = "CustomerSubscriptionTrialWillEnd";
                        break;
                    }
                case StripeEvents.InvoicePaymentSucceeded:
                    {
                        var invoice = Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());

                        var customer = await _subscriptionService.GetCustomerService()
                                                    .GetAsync(invoice.CustomerId);

                        var subscription = await _subscriptionService.GetSubscriptionService()
                                                    .GetAsync(invoice.CustomerId, invoice.SubscriptionId);
                        
                        var user = await _userManager.FindByEmailAsync(customer.Email);

                        if (user != null)
                        {
                            if (subscription.CurrentPeriodEnd != null) // successful renewal
                            {
                                user.ActiveUntil = (DateTime)subscription.CurrentPeriodEnd;
                            }
                            else
                            {
                                user.ActiveUntil = DateTime.Today;
                            }
                 

                            // here;
                            await _userManager.UpdateAsync(user);


                            message = "InvoicePaymentSucceeded";
                        }
                        
                        break;
                    }
                case StripeEvents.CustomerSubscriptionDeleted:
                    {
                        StripeDeleted deleted = Mapper<StripeDeleted>.MapFromJson(stripeEvent.Data.Object.ToString());

                        //TODO:


                        break;
                    }
                case StripeEvents.InvoicePaymentFailed:
                    {
                        var failedInvoice = Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());
                        //TODO: implement sending email to customer and customer service

                        message = "InvoicePaymentFailed";
                        break;
                    }
                default:
                    break;
            }

            //TODO: log Stripe eventid to StripeEvent table in application database
            return Ok(message);
        }


        private bool HasEventBeenProcessedPreviously(StripeEvent stripeEvent)
        {
            //lookup in your database's StripeEventLog by  eventid
            //if eventid exists return true
            return false;

        }

        private static async Task<StripeEvent> VerifyEventSentFromStripeAsync(StripeEvent stripeEvent)
        {
            return await new StripeEventService().GetAsync(stripeEvent.Id);
        }
    }
}
