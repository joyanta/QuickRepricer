using RethinkDb.Driver.Model;
using RethinkDb.Driver.Net;
using System.Threading.Tasks;

namespace QuickRepricer.Catalogue.Persistance
{
    public interface IRethinkDbConnectionFactory
    {
        Connection CreateConnection();
        void CloseConnection();
        RethinkDbOptions GetOptions();
        Task<Cursor<Change<T>>> GetFeed<T>(string table);

        Task<Cursor<T>> CurseThroughTable<T>(string table);
    }
}
