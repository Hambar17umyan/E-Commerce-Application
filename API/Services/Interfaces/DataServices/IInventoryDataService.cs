using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;

namespace API.Services.Interfaces.DataServices
{
    public interface IInventoryDataService : IDataService<Inventory>
    {
        Task<InnerResult> ReduceQuantityAsync(int inventoryId, int quantity);
        Task<InnerResult> AddQuantityAsync(int inventoryId, int quantity);
    }
}
