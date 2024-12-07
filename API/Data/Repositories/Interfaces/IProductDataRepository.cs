using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using FluentResults;

namespace API.Data.Repositories.Interfaces
{
    public interface IProductDataRepository : IDataRepository<Product>
    {
        Task<InnerResult> UpdatePriceAsync(Product product, decimal newPrice);
        Task<InnerResult> UpdatePriceAsync(int id, decimal newPrice);
    }
}
