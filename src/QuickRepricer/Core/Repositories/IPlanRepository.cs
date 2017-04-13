using QuickRepricer.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickRepricer.Core.Repositories
{
    public interface IPlanRepository
    {
        Task<Plan> GetPlanByIdAsync(int id);

        Task<IEnumerable<Plan>> GetPlansAsync();

        void Add(Plan plan);

        void AddRange(List<Plan> plans);
    }
}