using QuickRepricer.Catalogue.Core.Model;

namespace QuickRepricer.Catalogue.Persistance
{
    public interface IDbContext
    {
        void InitDatabase();
        void Reconfigure(int shards, int replicas);

        string InsertOrUpdateInventory(Inventory inventory);
    }
}
