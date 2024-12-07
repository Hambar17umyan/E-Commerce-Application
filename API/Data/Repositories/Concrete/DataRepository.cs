using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        public virtual async Task<InnerResult> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return InnerResult.Ok();
        }
        public virtual IEnumerable<T> GetAll() => _dbSet.AsEnumerable();
        public virtual InnerResult<T> GetBy(Func<T, bool> predicate)
        {
            var res = GetAll().FirstOrDefault(x => predicate(x));
            if (res is null)
                return InnerResult<T>.Fail($"{typeof(T).Name} not found!", HttpStatusCode.BadRequest);
            return res;
        }
        public virtual async Task<InnerResult> RemoveAsync(T entity)
        {
            var resp = GetBy(x => x.Id == entity.Id);
            if (resp.IsSuccess)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return InnerResult.Ok();
            }
            else
            {
                return InnerResult.Fail(resp.Errors, resp.StatusCode);
            }
        }
        public virtual async Task<InnerResult> UpdateAsync(Func<T, bool> predicate, Action<T> action)
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
                    return InnerResult.Fail("Exception was thrown! Here is the message:\n" + ex.Message);
                }
                await _context.SaveChangesAsync();
                return InnerResult.Ok();
            }
            else
            {
                return InnerResult.Fail(resp.Errors, resp.StatusCode);
            }

        }
    }
}
