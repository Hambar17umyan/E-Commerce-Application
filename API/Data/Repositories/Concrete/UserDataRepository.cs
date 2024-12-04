using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain;
using API.Services.Interfaces.DataServices;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Concrete
{
    public sealed class UserDataRepository : DataRepository<User>, IUserDataRepository
    {
        public UserDataRepository(ECommerceDbContext context) : base(context, context.Users) { }

        public override IEnumerable<User> GetAll()
        {
            return _dbSet.Include(x => x.Roles)
                .Include(x => x.Orders)
                .ThenInclude(x => x.LineItems)
                .ThenInclude(x => x.Product)
                .AsEnumerable();
        }
    }
}
