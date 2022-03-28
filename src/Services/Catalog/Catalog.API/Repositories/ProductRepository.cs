using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Filters;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts() =>
            await _context.Products.Find(x => true).ToListAsync();

        public async Task<Product> GetProduct(string id) => 
            await _context.Products.Find(RepositoryFilters.FilterById(id)).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category) =>
            await _context.Products.Find(RepositoryFilters.FilterByCategory(category)).ToListAsync();

        public async Task<IEnumerable<Product>> GetProductsByName(string name) =>
            await _context.Products.Find(RepositoryFilters.FilterByName(name)).ToListAsync();

        public async Task CreateProduct(Product product) =>
            await _context.Products.InsertOneAsync(product);
        
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(RepositoryFilters.FilterById(product.Id), product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _context
                                        .Products
                                        .DeleteOneAsync(RepositoryFilters.FilterById(id));

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
