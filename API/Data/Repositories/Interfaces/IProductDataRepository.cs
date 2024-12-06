using API.Models.Domain.Concrete;
using FluentResults;

namespace API.Data.Repositories.Interfaces
{
    public interface IProductDataRepository : IDataRepository<Product>
    {
        Task<Result> UpdatePriceAsync(Product product, decimal newPrice);
        Task<Result> UpdatePriceAsync(int id, decimal newPrice);
    }
}
