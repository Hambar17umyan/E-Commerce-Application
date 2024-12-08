using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Services.Interfaces.DataServices;

namespace API.Services.Concrete.DataServices
{
    public sealed class InventoryDataService : DataService<Inventory>, IInventoryDataService
    {
        private IInventoryDataRepository _repository;
        public InventoryDataService(IInventoryDataRepository repo) : base(repo) { _repository = repo; }

        public async Task<InnerResult> ReduceQuantityAsync(int inventoryId, int quantity) => await _repository.DecreaseQuantityAsync(inventoryId, quantity);
        public async Task<InnerResult> AddQuantityAsync(int inventoryId, int quantity) => await _repository.IncreaseQuantityAsync(inventoryId, quantity);
    }
}
