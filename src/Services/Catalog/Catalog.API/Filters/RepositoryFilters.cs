using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Filters
{
    public class RepositoryFilters
    {
        public static FilterDefinition<Product> FilterById(string id) =>
            Builders<Product>.Filter.Eq(x => x.Id, id);

        public static FilterDefinition<Product> FilterByName(string name) =>
            Builders<Product>.Filter.Eq(x => x.Name, name);

        public static FilterDefinition<Product> FilterByCategory(string category) =>
            Builders<Product>.Filter.Eq(x => x.Category, category);
    }
}
