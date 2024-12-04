using API.Data.Repositories.Concrete;
using API.Data.Repositories.Interfaces;
using API.Models.Domain;
using API.Services.Concrete.Control;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using FluentResults;

namespace API.Services.Concrete.DataServices
{
    public sealed class UserDataService : DataService<User>, IUserDataService
    {
        private IPasswordHashingService _passwordHashingService;

        public UserDataService(IUserDataRepository repo, IPasswordHashingService passwordHashingService) : base(repo)
        {
            _passwordHashingService = passwordHashingService;
        }
        public Result<User> GetByEmail(string email) => GetBy(x => x.Email == email);
        public Result<User> Authenticate(string email, string password)
        {
            var resp = GetByEmail(email);

            if (resp.IsSuccess)
            {
                var user = resp.Value;
                if (_passwordHashingService.Verify(password, user.PasswordHash))
                {
                    return Result.Ok(user);
                }
                else
                {
                    return Result.Fail("Incorrect password!");
                }
            }
            else
            {
                return Result.Fail(resp.Errors);
            }
        }
        public async Task<Result> RemoveAsync(string email)
        {
            var res = GetByEmail(email);
            if (res.IsSuccess)
            {
                await _repo.RemoveAsync(res.Value);
                return Result.Ok();
            }
            else return Result.Fail(res.Errors);
        }
        public async Task<Result> UpdateAsync(int id, Action<User> action) => await UpdateAsync(x => x.Id == id, action);
        public async Task<Result> UpdateAsync(string email, Action<User> action) => await UpdateAsync(x => x.Email == email, action);
    }
}
