using QuickRepricer.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickRepricer.Core.Services.Subscription.Plans
{
    public interface IPlanService
    {
        Task<Plan> FindAsync(int id);
        Task<IEnumerable<Plan>> ListAsync();
    }
}
