﻿using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
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
        public async Task<InnerResult> DecreaseQuantityAsync(int id, int numberOfOldItems)
        {
            var resp = GetById(id);
            if (resp.IsSuccess)
            {
                if (resp.Value.Quantity < numberOfOldItems)
                    return InnerResult.Fail("The number of inventories is less then specified!", System.Net.HttpStatusCode.Conflict);
                resp.Value.Quantity -= numberOfOldItems;
                await _context.SaveChangesAsync();
                return InnerResult.Ok();
            }
            else
            {
                return InnerResult.Fail(resp.Errors, resp.StatusCode);
            }
        }
        public Task<InnerResult> DecreaseQuantityAsync(Product product, int numberOfOldItems) => DecreaseQuantityAsync(product.Id, numberOfOldItems);
        public async Task<InnerResult> IncreaseQuantityAsync(int id, int numberOfNewItems)
        {
            var resp = GetBy(x => x.Id == id);
            if (resp.IsSuccess)
            {
                resp.Value.Quantity += numberOfNewItems;
                await _context.SaveChangesAsync();
                return InnerResult.Ok();
            }
            else
            {
                return InnerResult.Fail(resp.Errors, resp.StatusCode);
            }
        }
        public async Task<InnerResult> IncreaseQuantityAsync(Product product, int numberOfNewItems) => await IncreaseQuantityAsync(product.Id, numberOfNewItems);
    }
}
