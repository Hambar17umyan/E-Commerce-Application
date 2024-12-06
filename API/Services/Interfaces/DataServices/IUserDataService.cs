using API.Models.Domain.Concrete;
using FluentResults;

namespace API.Services.Interfaces.DataServices
{
    public interface IUserDataService : IDataService<User>
    {
        public Result<User> GetByEmail(string email);
        public Result<User> Authenticate(string email, string password);
        public Task<Result> RemoveAsync(string email);
        public Task<Result> UpdateAsync(int id, Action<User> action);
        public Task<Result> UpdateAsync(string email, Action<User> action);

    }
}
