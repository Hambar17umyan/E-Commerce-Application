using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using FluentResults;

namespace API.Data.Repositories.Interfaces
{
    public interface IInventoryDataRepository : IDataRepository<Inventory>
    {
        Task<InnerResult> IncreaseQuantityAsync(int id, int numberOfNewItems);
        Task<InnerResult> IncreaseQuantityAsync(Product product, int numberOfNewItems);
        Task<InnerResult> DecreaseQuantityAsync(int id, int numberOfOldItems);
        Task<InnerResult> DecreaseQuantityAsync(Product product, int numberOfOldItems);


    }
}
