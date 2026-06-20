using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository(ICatalogContext context) : IProductRepository, IBrandRepository, ITypeRepository
    {
        public async Task<Product> CreateProduct(Product product)
        {
            await context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deletedProduct = await context.Products.DeleteOneAsync(p => p.Id == id);
            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await context.Brands.Find(b => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await context.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByBrand(string brand)
        {
            return await context.Products.Find(p => p.Brand.Name == brand).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByName(string name)
        {
            return await context.Products.Find(p => p.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await context.Types.Find(t => true).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedProduct = await context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
    }
}
