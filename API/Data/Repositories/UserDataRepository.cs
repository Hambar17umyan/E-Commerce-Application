using API.Data.Db;
using API.Models.Domain;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class UserDataRepository
    {
        public UserDataRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        private ECommerceDbContext _context;

        public async Task<Result> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(x => x.Orders)
                .Include(x => x.Cart)
                .Include(x => x.Roles).AsEnumerable();
        }
        public Result<User> GetBy(Func<User, bool> predicate)
        {
            var user = GetAll().FirstOrDefault(x => predicate(x));
            if (user == null)
            {
                return Result.Fail("User not found!");
            }
            else
            {
                return Result.Ok(user);
            }
        }
        public async Task<Result> RemoveAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }
        public async Task<Result> UpdateAsync(Func<User, bool> predicate, Action<User> action)
        {
            var resp = GetBy(predicate);

            if (resp.IsSuccess)
            {
                action(resp.Value);
                await _context.SaveChangesAsync();
                return Result.Ok();
            }
            return Result.Fail("User not found!");
        }
    }
}
