using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Concrete
{
    public abstract class DataRepository<T> : IDataRepository<T> where T : class, IDomain
    {
        protected ECommerceDbContext _context;
        protected DbSet<T> _dbSet;
        public DataRepository(ECommerceDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public virtual async Task<Result> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }
        public virtual IEnumerable<T> GetAll() => _dbSet.AsEnumerable();
        public virtual Result<T> GetBy(Func<T, bool> predicate)
        {
            var res = GetAll().FirstOrDefault(x => predicate(x));
            if (res is null)
                return Result.Fail($"{typeof(T).Name} not found!");
            return res;
        }
        public virtual async Task<Result> RemoveAsync(T entity)
        {
            var resp = GetBy(x => x.Id == entity.Id);
            if (resp.IsSuccess)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return Result.Ok();
            }
            else
            {
                return Result.Fail(resp.Errors);
            }
        }
        public virtual async Task<Result> UpdateAsync(Func<T, bool> predicate, Action<T> action)
        {
            var resp = GetBy(predicate);
            if (resp.IsSuccess)
            {
                try
                {
                    action(resp.Value);
                }
                catch (Exception ex)
                {
                    return Result.Fail("Exception was thrown! Here is the message:\n" + ex.Message);
                }
                await _context.SaveChangesAsync();
                return Result.Ok();
            }
            else
            {
                return Result.Fail(resp.Errors);
            }

        }
    }
}
