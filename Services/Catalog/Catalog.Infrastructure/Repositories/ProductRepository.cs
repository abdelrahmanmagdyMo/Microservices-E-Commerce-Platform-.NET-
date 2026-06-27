using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
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

        public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                var typeFilter = builder.Eq(p => p.Type.Id, catalogSpecParams.TypeId);
                filter &= typeFilter;
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                var brandFilter = builder.Eq(p => p.Brand.Id, catalogSpecParams.BrandId);
                filter &= brandFilter;
            }
            var totalItems = await context.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecParams, filter);
            return new Pagination<Product>(
                catalogSpecParams.PageSize,
                catalogSpecParams.PageIndex,
                (int)totalItems,
                data);
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
        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            var sortDefn = Builders<Product>.Sort.Ascending("Name");
            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                switch (catalogSpecParams.Sort)
                {
                    case "priceAsc":
                        sortDefn = Builders<Product>.Sort.Ascending("Price");
                        break;
                    case "priceDesc":
                        sortDefn = Builders<Product>.Sort.Descending("Price");
                        break;
                    default:
                        sortDefn = Builders<Product>.Sort.Ascending("Name");
                        break;
                }
            }
            return await context.Products
                .Find(filter)
                .Sort(sortDefn)
                .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                .Limit(catalogSpecParams.PageSize)
                .ToListAsync();
        }
    }
}
