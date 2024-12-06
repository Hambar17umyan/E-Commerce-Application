using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain.Concrete;
using FluentResults;

namespace API.Data.Repositories.Concrete
{
    public sealed class ProductDataRepository : DataRepository<Product>, IProductDataRepository
    {
        public ProductDataRepository(ECommerceDbContext context) : base(context, context.Products) { }
        public async Task<Result> UpdatePriceAsync(Product product, decimal newPrice) => await UpdatePriceAsync(product.Id, newPrice);
        public async Task<Result> UpdatePriceAsync(int id, decimal newPrice) => await UpdateAsync(x => x.Id == id, x => x.Price = newPrice);
    }
}
