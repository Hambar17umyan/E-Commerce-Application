using API.Models.Domain;
using FluentResults;

namespace API.Services.Interfaces.DataServices
{
    public interface IUserDataService : IDataService<User>
    {
        public Task<Result<User>> GetByEmailAsync(string email);
        public Task<Result<User>> AuthenticateAsync(string email, string password);
        public Task<Result> RemoveAsync(string email);
        public Task<Result> UpdateAsync(int id, Action<User> action);
        public Task<Result> UpdateAsync(string email, Action<User> action);

    }
}
