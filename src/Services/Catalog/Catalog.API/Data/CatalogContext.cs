using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {

        public CatalogContext(IConfiguration configuration)
        {
            Products = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"))
                        .GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"))
                        .GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        }

        public IMongoCollection<Product> Products { get; }
    }
}
