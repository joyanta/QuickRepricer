using Microsoft.EntityFrameworkCore;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRepricer.Persistence.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private ApplicationDbContext _context;

        public MerchantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Merchant> GetMechantByIdAsync(int merchantId)
        {
            return await (from m in _context.Merchants
                                    .Include(item => item.MarketPlaces)
                                    where m.MerchantId == merchantId
                                    select m).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Merchant>> GetMechantsAsync()
        {
            return await (from m in _context.Merchants
                                    .Include(item => item.MarketPlaces)
                                    select m).ToListAsync();
        }
        
        public void Add(Merchant merchant)
        {
            _context.Merchants.Add(merchant);
        }

        public void AddRange(List<Merchant> merchants)
        {
            _context.Merchants.AddRange(merchants);
        }
    }
}
