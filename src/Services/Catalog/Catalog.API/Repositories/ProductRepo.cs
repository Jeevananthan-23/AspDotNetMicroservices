using Catalog.API.Data;
using Catalog.API.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepo:IProductRepo
    {
        private readonly ICatalogContext _context;

        public ProductRepo(ICatalogContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
             await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _context
                                    .Products.DeleteOneAsync(Builders<Product>.Filter.Eq(p=>p.Id,id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context
                            .Products
                             .Find(p => p.Id == id)
                             .FirstOrDefaultAsync();
        }
                                

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {

            return await _context
                              .Products
                               .Find(Builders<Product>.Filter.Eq(p=>p.Category,categoryName))
                               .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _context
                              .Products
                               .Find(Builders<Product>.Filter.Eq(p => p.Name , name))
                               .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                    .Products
                                    .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
