using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using System.Linq;
using QuickRepricer.Catalogue.Core.Model;

namespace QuickRepricer.Catalogue.Persistance
{
    public class RethinkDbStore : IDbContext
    {
        private static IRethinkDbConnectionFactory _ConnectionFactory;
        private static RethinkDB R = RethinkDB.R;

        private string _dbName;

        public RethinkDbStore(IRethinkDbConnectionFactory connectionFactory)
        {
            _ConnectionFactory = connectionFactory;
            _dbName = connectionFactory.GetOptions().Database;
        }

        public void InitDatabase()
        {
            CreateDb(_dbName);
            CreateTable(_dbName, nameof(Inventory));
            CreateIndex(_dbName, nameof(Inventory), nameof(Inventory.MerchantId));
            

        }

        public void Reconfigure(int shards, int replicas)
        {
            var conn = _ConnectionFactory.CreateConnection();
            var tables = R.Db(_dbName)
                            .TableList()
                            .Run(conn);

            foreach (string table in tables)
            {
                R.Db(_dbName).Table(table)
                    .Reconfigure()
                    .OptArg("shards", shards)
                    .OptArg("replicas", replicas)
                    .Run(conn);

                R.Db(_dbName).Table(table)
                    .Wait_()
                    .Run(conn);
            }
        }

        public string InsertOrUpdateInventory(Inventory inventory)
        {
            var conn = _ConnectionFactory.CreateConnection();

            Cursor<Inventory> all = R.Db(_dbName)
                                    .Table(nameof(Inventory))
                                    .GetAll(inventory.MerchantId)[new { index = nameof(Inventory.MerchantId) }]
                                    .Run<Inventory>(conn);

            var items = all.ToList();

            if (items.Count > 0)
            {
                // update 
                R.Db(_dbName)
                    .Table(nameof(Inventory))
                    .GetAll(items.First().MerchantId)[new { index = nameof(Inventory.MerchantId) }]
                    .Update(inventory)
                    .RunResult(conn);

                return items.First().MerchantId;
            }
            else
            {
                // insert 
                var result = R.Db(_dbName)
                        .Table(nameof(Inventory))
                        .Insert(inventory)
                        .RunResult(conn);

                return result.GeneratedKeys.First().ToString();
            }
        }


        private void CreateDb(string dbName)
        {
            var conn = _ConnectionFactory.CreateConnection();
            var exists = R.DbList()
                        .Contains(db => db == dbName)
                        .Run(conn);

            if (!exists)
            {
                R.DbCreate(dbName)
                    .Run(conn);

                R.Db(dbName)
                    .Wait_()
                    .Run(conn);
            }
        }

        private void CreateTable(string dbName, string tableName)
        {
            var conn = _ConnectionFactory.CreateConnection();
            var exists = R.Db(dbName)
                            .TableList()
                            .Contains(t => t == tableName)
                            .Run(conn);

            if (!exists)
            {
                R.Db(dbName)
                    .TableCreate(tableName)
                  //  .OptArg("primary_key", "Asim")
                    .Run(conn);

                R.Db(dbName)
                    .Table(tableName)
                    .Wait_()
                    .Run(conn);
            }
        }

        private void CreateIndex(string dbName, string tableName, string indexName)
        {
            var conn = _ConnectionFactory.CreateConnection();
            var exists = R.Db(dbName)
                .Table(tableName)
                .IndexList()
                .Contains(t => t == indexName)
                .Run(conn);

            if (!exists)
            {
                R.Db(dbName)
                    .Table(tableName)
                    .IndexCreate(indexName)
                    .Run(conn);

                R.Db(dbName)
                    .Table(tableName)
                    .IndexWait(indexName)
                    .Run(conn);
            }
        }
    }
}