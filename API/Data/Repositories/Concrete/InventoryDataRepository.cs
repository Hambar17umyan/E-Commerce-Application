using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Concrete
{
    public sealed class InventoryDataRepository : DataRepository<Inventory>, IInventoryDataRepository
    {
        public override IEnumerable<Inventory> GetAll()
        {
            return _dbSet
                .Include(x => x.Product)
                .AsEnumerable();
        }
        public InventoryDataRepository(ECommerceDbContext context) : base(context, context.Inventories) { }
        public async Task<Result> DecreaseQuantityAsync(int id, int numberOfOldItems) => await IncreaseQuantityAsync(id, -numberOfOldItems);
        public Task<Result> DecreaseQuantityAsync(Product product, int numberOfOldItems)
        {
            return DecreaseQuantityAsync(product.Id, numberOfOldItems);
        }
        public async Task<Result> IncreaseQuantityAsync(int id, int numberOfNewItems)
        {
            var resp = GetBy(x => x.Id == id);
            if(resp.IsSuccess)
            {
                resp.Value.Quantity += numberOfNewItems;
                await _context.SaveChangesAsync();
                return Result.Ok();
            }
            else
            {
                return Result.Fail(resp.Errors);
            }
        }
        public async Task<Result> IncreaseQuantityAsync(Product product, int numberOfNewItems) => await IncreaseQuantityAsync(product.Id, numberOfNewItems);
    }
}
