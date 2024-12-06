using API.Data.Repositories.Interfaces;
using API.Models.Domain.Concrete;
using API.Services.Interfaces.DataServices;

namespace API.Services.Concrete.DataServices
{
    public sealed class InventoryDataService : DataService<Inventory>, IInventoryDataService
    {
        public InventoryDataService(IInventoryDataRepository repo) : base(repo) { }
    }
}
