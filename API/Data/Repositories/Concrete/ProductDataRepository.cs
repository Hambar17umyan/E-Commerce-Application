using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Concrete
{
    public sealed class ProductDataRepository : DataRepository<Product>, IProductDataRepository
    {
        public ProductDataRepository(ECommerceDbContext context) : base(context, context.Products) { }
        public async Task<InnerResult> UpdatePriceAsync(Product product, decimal newPrice) => await UpdatePriceAsync(product.Id, newPrice);
        public async Task<InnerResult> UpdatePriceAsync(int id, decimal newPrice) => await UpdateAsync(x => x.Id == id, x => x.Price = newPrice);
    }
}
