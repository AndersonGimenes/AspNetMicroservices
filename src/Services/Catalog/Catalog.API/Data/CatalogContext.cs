using Catalog.API.IoC.Factories;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var dataBase = MongoClientFactory.GetInstanceWithDataBase(configuration);

            Products = MongoClientFactory.CreateColleciton<Product>(dataBase, configuration);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
