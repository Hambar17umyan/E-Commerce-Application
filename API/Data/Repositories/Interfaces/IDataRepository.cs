using FluentResults;

namespace API.Data.Repositories.Interfaces
{
    public interface IDataRepository<T>
    {
        public Task<Result> AddAsync(T entity);
        public IEnumerable<T> GetAll();
        public Task<Result<T>> GetBy(Func<T, bool> predicate);
        public Task<Result> RemoveAsync(T entity);
        public Task<Result> UpdateAsync(Func<T, bool> predicate, Action<T> action);
        public IQueryable<T> GetAllAsQueryable();
    }
}
