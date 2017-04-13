using QuickRepricer.Core;
using QuickRepricer.Core.Repositories;
using QuickRepricer.Persistence.Repositories;
using System.Threading.Tasks;

namespace QuickRepricer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public IPlanRepository Plans { get; private set; }
        public IFeatureRepository Features { get; private set; }
        public IMerchantRepository Merchants { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context =  context;
            Plans = new PlanRepository(_context);
            Features = new FeatureRepository(_context);
            Merchants = new MerchantRepository(_context);
        }
        
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
