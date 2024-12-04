using API.Data.Repositories.Interfaces;
using API.Models.Domain.Interfaces;
using API.Services.Interfaces.DataServices;
using FluentResults;

namespace API.Services.Concrete.DataServices
{
    public abstract class DataService<T> : IDataService<T> where T : IDomain
    {
        protected IDataRepository<T> _repo;

        public DataService(IDataRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual async Task<Result> AddAsync(T entity) => await _repo.AddAsync(entity);
        public virtual Result<IEnumerable<T>> GetAll() => Result.Ok(_repo.GetAll());
        public virtual Result<IEnumerable<T>> GetAllThat(Func<T, bool> predicate) => Result.Ok(_repo.GetAll().Where(predicate).AsEnumerable());
        public virtual Result<T> GetBy(Func<T, bool> predicate) => _repo.GetBy(predicate);
        public virtual Result<T> GetById(int id) => GetBy(x => x.Id == id);
        public virtual Task<Result> RemoveAsync(T entity) => _repo.RemoveAsync(entity);
        public virtual async Task<Result> RemoveAsync(int productId)
        {
            var resp = _repo.GetBy(x => x.Id == x.Id);
            if(resp.IsFailed)
            {
                return Result.Fail(resp.Errors);
            }
            return await _repo.RemoveAsync(resp.Value);
        }
        public virtual async Task<Result> UpdateAsync(Func<T, bool> predicate, Action<T> action) => await _repo.UpdateAsync(predicate, action);
    }
}
