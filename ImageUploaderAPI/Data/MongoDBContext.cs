using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ImageUploaderAPI.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
            _database = client.GetDatabase("ImageUploaderDB");
        }

        public IMongoDatabase Database => _database;
    }
}
