using FluentResults;

namespace API.Services.Interfaces.DataServices
{
    public interface IDataService<T>
    {
        Task<Result> AddAsync(T entity);
        Result<IEnumerable<T>> GetAll();
        Result<IEnumerable<T>> GetAllThat(Func<T, bool> predicate);
        Result<T> GetById(int id);
        Result<T> GetBy(Func<T, bool> predicate);
        Task<Result> RemoveAsync(T entity);
        Task<Result> RemoveAsync(int productId);
        Task<Result> UpdateAsync(Func<T, bool> predicate, Action<T> action);
    }
}
