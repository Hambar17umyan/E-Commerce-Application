using API.Models.Control.ResultModels;
using FluentResults;

namespace API.Services.Interfaces.DataServices
{
    public interface IDataService<T>
    {
        Task<InnerResult> AddAsync(T entity);
        InnerResult<IEnumerable<T>> GetAll();
        InnerResult<IEnumerable<T>> GetAllThat(Func<T, bool> predicate);
        InnerResult<T> GetById(int id);
        InnerResult<T> GetBy(Func<T, bool> predicate);
        Task<InnerResult> RemoveAsync(T entity);
        Task<InnerResult> RemoveAsync(int productId);
        Task<InnerResult> UpdateAsync(Func<T, bool> predicate, Action<T> action);
    }
}
