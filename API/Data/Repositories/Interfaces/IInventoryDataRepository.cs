using API.Models.Domain.Concrete;
using FluentResults;

namespace API.Data.Repositories.Interfaces
{
    public interface IInventoryDataRepository : IDataRepository<Inventory>
    {
        Task<Result> IncreaseQuantityAsync(int id, int numberOfNewItems);
        Task<Result> IncreaseQuantityAsync(Product product, int numberOfNewItems);
        Task<Result> DecreaseQuantityAsync(int id, int numberOfOldItems);
        Task<Result> DecreaseQuantityAsync(Product product, int numberOfOldItems);


    }
}
