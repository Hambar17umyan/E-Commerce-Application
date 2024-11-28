using API.Data.Repositories;
using API.Models.Domain;
using API.Services.Concrete.Control;
using FluentResults;

namespace API.Services.Concrete.DataServices
{
    public class UserDataService
    {

        private UserDataRepository _repo;
        private PasswordHashingService _passwordHashingService;

        public UserDataService(UserDataRepository repo, PasswordHashingService passwordHashingService)
        {
            _repo = repo;
            _passwordHashingService = passwordHashingService;
        }
        public Result<User> GetById(int id)
        {
            return _repo.GetBy(x => x.Id == id);
        }
        public Result<User> GetByEmail(string email)
        {
            return _repo.GetBy(x => x.Email == email);
        }
        public Result<User> GetBy(Func<User, bool> predicate) => _repo.GetBy(predicate);
        public Result<IEnumerable<User>> GetAll()
        {
            var users = _repo.GetAll();
            return Result.Ok(users);
        }
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
        public async Task<Result> RemoveAsync(User user) => await _repo.RemoveAsync(user);
        public async Task<Result> RemoveAsync(int userId)
        {
            var res = GetById(userId);
            if (res.IsSuccess)
            {
                await _repo.RemoveAsync(res.Value);
                return Result.Ok();
            }
            else return Result.Fail(res.Errors);
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
        public async Task<Result> UpdateAsync(Func<User, bool> predicate, Action<User> action) => await _repo.UpdateAsync(predicate, action);
        public async Task<Result> UpdateAsync(int id, Action<User> action) => await UpdateAsync(x => x.Id == id, action);
        public async Task<Result> UpdateAsync(string email, Action<User> action) => await UpdateAsync(x => x.Email == email, action);
        public async Task<Result> AddAsync(User user) => await _repo.AddAsync(user);

    }
}
