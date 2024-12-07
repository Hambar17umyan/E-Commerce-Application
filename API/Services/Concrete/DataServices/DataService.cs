using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Interfaces;
using API.Models.Response.Output;
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

        public virtual async Task<InnerResult> AddAsync(T entity) => await _repo.AddAsync(entity);
        public virtual InnerResult<IEnumerable<T>> GetAll() => InnerResult<IEnumerable<T>>.Ok(_repo.GetAll());
        public virtual InnerResult<IEnumerable<T>> GetAllThat(Func<T, bool> predicate) => InnerResult<IEnumerable<T>>.Ok(_repo.GetAll().Where(predicate).AsEnumerable());
        public virtual InnerResult<T> GetBy(Func<T, bool> predicate) => _repo.GetBy(predicate);
        public virtual InnerResult<T> GetById(int id) => GetBy(x => x.Id == id);
        public virtual Task<InnerResult> RemoveAsync(T entity) => _repo.RemoveAsync(entity);
        public virtual async Task<InnerResult> RemoveAsync(int productId)
        {
            var resp = _repo.GetBy(x => x.Id == x.Id);
            if(resp.IsFailed)
            {
                return InnerResult.Fail(resp.Errors, resp.StatusCode);
            }
            return await _repo.RemoveAsync(resp.Value);
        }
        public virtual async Task<InnerResult> UpdateAsync(Func<T, bool> predicate, Action<T> action) => await _repo.UpdateAsync(predicate, action);
    }
}
