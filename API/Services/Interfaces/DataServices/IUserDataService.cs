using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using FluentResults;

namespace API.Services.Interfaces.DataServices
{
    public interface IUserDataService : IDataService<User>
    {
        public InnerResult<User> GetByEmail(string email);
        public InnerResult<User> Authenticate(string email, string password);
        public Task<InnerResult> RemoveAsync(string email);
        public Task<InnerResult> UpdateAsync(int id, Action<User> action);
        public Task<InnerResult> UpdateAsync(string email, Action<User> action);
        public Task<InnerResult> AddToCartAsync(int id, Product product, int quantity);
        public Task<InnerResult> AddToCartAsync(int userId, int productId, int quantity);
    }
}
