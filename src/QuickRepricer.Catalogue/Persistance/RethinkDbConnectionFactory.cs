using Microsoft.Extensions.Options;
using RethinkDb.Driver;
using RethinkDb.Driver.Model;
using RethinkDb.Driver.Net;
using System.Threading.Tasks;

namespace QuickRepricer.Catalogue.Persistance
{
    public class RethinkDbConnectionFactory : IRethinkDbConnectionFactory
    {
        private static RethinkDB R = RethinkDB.R;
        private Connection conn;
        private RethinkDbOptions _options;

        public RethinkDbConnectionFactory(IOptions<RethinkDbOptions> options)
        {
            _options = options.Value;
        }

        public void CloseConnection()
        {
            if (conn != null && conn.Open)
            {
                conn.Close(false);
            }
        }

        public Connection CreateConnection()
        {
            if (conn == null) {
                conn = R.Connection()
                    .Hostname(_options.Host)
                    .Port(_options.Port)
                    .Timeout(_options.Timeout)
                    .Connect();
            }

            if (!conn.Open) {
                conn.Reconnect();
            }
            return conn;

        }

        public RethinkDbOptions GetOptions()
        {
            return _options;
        }

        public async Task<Cursor<Change<T>>> GetFeed<T>(string table)
        {
            var feed = await R.Db(_options.Database).Table(table)
                                 .Changes().RunChangesAsync<T>(CreateConnection());

            return feed;
        }


        public async Task<Cursor<T>> CurseThroughTable<T>(string table)
        {
            var cursor = await R.Db(_options.Database)
                .Table(table)
                .RunCursorAsync<T>(CreateConnection());

            return cursor;
        }
    }
}
