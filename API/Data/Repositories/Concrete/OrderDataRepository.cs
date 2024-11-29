using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain;

namespace API.Data.Repositories.Concrete
{
    public sealed class OrderDataRepository : DataRepository<Order>, IOrderDataRepository
    {
        public OrderDataRepository(ECommerceDbContext context) : base(context, context.Orders)
        {
            
        }
    }
}
