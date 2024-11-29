using API.Data.Repositories.Interfaces;
using API.Models.Domain.Interfaces;
using API.Services.Interfaces.DataServices;
using FluentResults;

namespace API.Services.Concrete.DataServices
{
    public class DataService<T> : IDataService<T> where T : IDomain
    {
        protected IDataRepository<T> _repo;

        public DataService(IDataRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual async Task<Result> AddAsync(T entity) => await _repo.AddAsync(entity);
        public virtual Result<IEnumerable<T>> GetAll() => Result.Ok(_repo.GetAll());
        public virtual Result<IEnumerable<T>> GetAllThat(Func<T, bool> predicate) => Result.Ok(_repo.GetAllAsQueryable().Where(predicate).AsEnumerable());
        public virtual async Task<Result<T>> GetByAsync(Func<T, bool> predicate) => await _repo.GetBy(predicate);
        public virtual async Task<Result<T>> GetByIdAsync(int id) => await GetByAsync(x => x.Id == id);
        public virtual Result<IEnumerable<T>> GetWithQuery(Func<IQueryable<T>, IEnumerable<T>> query)
        {
            IEnumerable<T> res;
            try
            {
                res = query(_repo.GetAllAsQueryable());
                return Result.Ok(res);
            }
            catch (Exception ex)
            {
                return Result.Fail("Exception was thrown! Here is the message:\n" + ex.Message);
            }
        }
        public virtual Task<Result> RemoveAsync(T entity) => _repo.RemoveAsync(entity);
        public virtual async Task<Result> RemoveAsync(int productId)
        {
            var resp = await _repo.GetBy(x => x.Id == x.Id);
            if(resp.IsFailed)
            {
                return Result.Fail(resp.Errors);
            }
            return await _repo.RemoveAsync(resp.Value);
        }
        public virtual async Task<Result> UpdateAsync(Func<T, bool> predicate, Action<T> action) => await _repo.UpdateAsync(predicate, action);
    }
}
