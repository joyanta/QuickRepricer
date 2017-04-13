using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Linq;
using Stripe;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.Services.Subscription;

namespace QuickRepricer.Services.Subscription
{
    public class SubscriptionService : ISubscriptionService
    {
        private IConfigurationRoot _configuration;
        private StripeCustomerService _customerService;
        private StripeSubscriptionService _subscriptionService;
        private UserManager<ApplicationUser> _userManager;

        public SubscriptionService(UserManager<ApplicationUser> userManager, IConfigurationRoot configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            
            _customerService = new StripeCustomerService(_configuration["PaymentSettings:Stripe:SecretKey"]);
            _subscriptionService = new StripeSubscriptionService(_configuration["PaymentSettings:Stripe:SecretKey"]);
        }


        public StripeSubscriptionService GetSubscriptionService()
        {
            
            return _subscriptionService;
        }

        public StripeCustomerService GetCustomerService()
        {
            return _customerService;
        }


        public async Task CreateOrUpdateAsync(string userName, Plan plan, string stripeToken)
        {
            var user = await _userManager.FindByNameAsync(userName);

            user.PlanId = plan.Id;

            if (string.IsNullOrEmpty(user.StripeCustomerId))  //first time customer with no previous paid subscription??
            {
                var customerOptions = new StripeCustomerCreateOptions()
                {
                    Email = user.Email,
                    PlanId = plan.ExternalId,
                    SourceToken = stripeToken
                };

                //case 1: new customer && ¬Active User (i.e. after trail preiod) --> nothing to do 

                //case 2: new customer && Active user (i.e. you can only be active user and a new customer 
                //  <--> trail period has not ended) 
                if (user.ActiveUntil > DateTime.Now)
                {
                    // set the trail period end date;
                    customerOptions.TrialEnd = user.ActiveUntil;
                }

                var stripeCustomer = await _customerService.CreateAsync(customerOptions);
                user.StripeCustomerId = stripeCustomer.Id;

                

                //get the first and only subscription;
                var subscriptionId = stripeCustomer.StripeSubscriptionList.Data.FirstOrDefault().Id;
                user.StripeSubscriptionId = subscriptionId;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(user.StripeSubscriptionId))
                {
                    var createOptions = new StripeSubscriptionCreateOptions()
                    {
                        PlanId = plan.ExternalId
                    };
                    
                    if (user.ActiveUntil > DateTime.Now)
                    {
                        createOptions.TrialEnd = user.ActiveUntil;
                    }
                    
                    var subscription = await _subscriptionService.CreateAsync(user.StripeCustomerId, plan.ExternalId, createOptions);
                    user.StripeSubscriptionId = subscription.Id;
                }
                else
                {
                    // get the current subscription
                    var subscription = await _subscriptionService.GetAsync(user.StripeCustomerId, user.StripeSubscriptionId);

                    var subscriptionUpdateOptions = new StripeSubscriptionUpdateOptions()
                    {
                        PlanId = plan.ExternalId
                    };

                    // MAY not need it
                    if (subscription.TrialEnd.HasValue && subscription.TrialEnd > DateTime.Now)
                    {
                        subscriptionUpdateOptions.TrialEnd = subscription.TrialEnd;
                    }


                    await _subscriptionService.UpdateAsync(user.StripeCustomerId, subscription.Id, subscriptionUpdateOptions);

                }
            }

            await _userManager.UpdateAsync(user);
        }
    }
}
