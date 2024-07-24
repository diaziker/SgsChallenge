using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SGS.Domain.Entities;
using SGS.Infrastructure.Settings;

namespace SGS.Infrastructure.Databases
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptionsMonitor<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.CurrentValue.ConnectionString);
            _database = client.GetDatabase(databaseSettings.CurrentValue.DatabaseName);
        }

        public IMongoCollection<EntityProduct> Products => _database.GetCollection<EntityProduct>("Products");
    }
}
