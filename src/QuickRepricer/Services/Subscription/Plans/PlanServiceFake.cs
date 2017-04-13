using QuickRepricer.Core.Models;
using QuickRepricer.Core.Services.Subscription.Plans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace QuickRepricer.Services.Subscription.Plans
{
    public class PlanServiceFake : IPlanService
    {
        public Task<Plan> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Plan>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
