using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Concrete
{
    public sealed class OrderDataRepository : DataRepository<Order>, IOrderDataRepository
    {
        public OrderDataRepository(ECommerceDbContext context) : base(context, context.Orders)
        {
            
        }

        public override IEnumerable<Order> GetAll()
        {
            return _dbSet.Include(o => o.LineItems)
                .ThenInclude(li=>li.Product)
                .AsEnumerable();
        }
    }
}
