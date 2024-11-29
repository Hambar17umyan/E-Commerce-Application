using FluentResults;

namespace API.Services.Interfaces.DataServices
{
    public interface IDataService<T>
    {
        Task<Result> AddAsync(T entity);
        Result<IEnumerable<T>> GetAll();
        Result<IEnumerable<T>> GetAllThat(Func<T, bool> predicate);
        Result<IEnumerable<T>> GetWithQuery(Func<IQueryable<T>, IEnumerable<T>> query);
        Task<Result<T>> GetByAsync(Func<T, bool> predicate);
        Task<Result<T>> GetByIdAsync(int id);
        Task<Result> RemoveAsync(T entity);
        Task<Result> RemoveAsync(int productId);
        Task<Result> UpdateAsync(Func<T, bool> predicate, Action<T> action);
    }
}
