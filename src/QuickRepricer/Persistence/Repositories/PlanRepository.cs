using Microsoft.EntityFrameworkCore;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.Repositories;
using QuickRepricer.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRepricer.Persistence.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private ApplicationDbContext _context;

        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Plan> GetPlanByIdAsync(int id)
        {
            return await (from p in _context.Plans.Include(item => item.Features)
                          where p.Id == id
                          select p).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansAsync()
        {
            return await (from p in _context.Plans.Include(item => item.Features)
                          orderby p.DisplayOrder
                          select p).ToListAsync();
        }

        public void Add(Plan plan)
        {
            _context.Add(plan);
        }

        public void AddRange(List<Plan> plans)
        {
            _context.AddRange(plans);
        }
    }
}
