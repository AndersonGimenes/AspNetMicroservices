using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.IoC.Factories
{
    public static class MongoClientFactory
    {
        public static IMongoDatabase GetInstanceWithDataBase(IConfiguration configuration) =>
            new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"))
                .GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        public static IMongoCollection<T> CreateColleciton<T>(IMongoDatabase database, IConfiguration configuration) =>
            database.GetCollection<T>(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
    }
}
