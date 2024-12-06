using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain.Concrete;
using API.Services.Interfaces.DataServices;

namespace API.Services.Concrete.DataServices
{
    public sealed class OrderDataService : DataService<Order>, IOrderDataService
    {
        public OrderDataService(IOrderDataRepository repo) : base(repo)
        {

        }
    }
}
