using QuickRepricer.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickRepricer.Core.Repositories
{
    public interface IMerchantRepository
    {
        Task<Merchant> GetMechantByIdAsync(int merchantId);

        Task<IEnumerable<Merchant>> GetMechantsAsync();

        void Add(Merchant merchant);

        void AddRange(List<Merchant> merchants);
    }
}