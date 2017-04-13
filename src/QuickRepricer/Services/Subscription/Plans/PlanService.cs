using Microsoft.Extensions.Configuration;
using QuickRepricer.Core;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.Services.Subscription.Plans;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRepricer.Services.Subscription.Plans
{
    public class PlanService : IPlanService
    {
        private IConfigurationRoot _configuration;

        private StripePlanService _stripePlanService;

        private IUnitOfWork _unitOfWork;

        public PlanService(IConfigurationRoot configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _stripePlanService = new StripePlanService(_configuration["PaymentSettings:Stripe:SecretKey"]);
            _unitOfWork = unitOfWork;
        }

        public async Task<Plan> FindAsync(int id)
        {
            var plan = await _unitOfWork.Plans.GetPlanByIdAsync(id);
            var stripePlan = await _stripePlanService.GetAsync(plan.ExternalId);

            StripePlanToPlan(plan, stripePlan);
            return plan;
        }


        public async Task<IEnumerable<Plan>> ListAsync()
        {
            var plans = await _unitOfWork.Plans.GetPlansAsync();
            var stripePlans = (from p in await _stripePlanService.ListAsync() select p).ToList();

            if (stripePlans != null)
            {
                foreach (var plan in plans)
                {
                    var stripePlan = stripePlans.Single(p => p.Id == plan.ExternalId);
                    StripePlanToPlan(plan, stripePlan);
                }
            }

            return plans;
        }
        

        private static void StripePlanToPlan(Plan plan, StripePlan stripePlan)
        {
            plan.Name = stripePlan.Name;
            plan.AmountInCents = stripePlan.Amount;
            plan.Currency = stripePlan.Currency;
            plan.Interval = stripePlan.Interval;
            plan.TrialPeriodDays = stripePlan.TrialPeriodDays;
            plan.Info = stripePlan.Metadata["info"];
        }
    }
}
