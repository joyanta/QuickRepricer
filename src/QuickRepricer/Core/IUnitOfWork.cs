using QuickRepricer.Core.Repositories;
using System.Threading.Tasks;

namespace QuickRepricer.Core
{
    public interface IUnitOfWork
    {
        IPlanRepository Plans { get; }
        IFeatureRepository Features { get; }
        IMerchantRepository Merchants { get; }

        Task CompleteAsync();
    }
}
