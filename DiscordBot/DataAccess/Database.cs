using SQLite;
using System.Threading.Tasks;

namespace DiscordBot.DataAccess {
    public class Database {
        readonly SQLiteAsyncConnection database;

        //public Database(string dbPath)
        //{
        //    database = new SQLiteAsyncConnection(dbPath, false);
        //    database.CreateTableAsync<Worker>().Wait();
        //    database.CreateTableAsync<Accelerometer>().Wait();
        //}

        //public Task<Worker> GetWorkerByAliasAsync(string alias)
        //{
        //    return database.Table<Worker>().FirstOrDefaultAsync(x => x.Alias == alias);
        //}

        //public Task<int> InsertWorkerAsync(Worker worker)
        //{
        //    return database.InsertAsync(worker);
        //}
    }
}
