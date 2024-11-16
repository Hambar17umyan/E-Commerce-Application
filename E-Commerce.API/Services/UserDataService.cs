using E_Commerce.API.Data.Repositories;
using E_Commerce.API.Models.DomainModels;
using E_Commerce.API.Models.DTOs;
using E_Commerce.API.Models.DTOs.Enums;

namespace E_Commerce.API.Services
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
        public ResponseModel<User> GetById(int id)
        {
            return _repo.GetBy(x => x.Id == id);
        }
        public ResponseModel<User> GetByEmail(string? email)
        {
            return _repo.GetBy(x => x.Email == email);
        }
        public ResponseModel<User> GetBy(Func<User, bool> predicate) => _repo.GetBy(predicate);
        public ResponseModel<User> Authenticate(string email, string password)
        {
            var user = (GetByEmail(email)).Result;
            if (user is null)
            {
                return ResponseModel<User>.GetFail(ResponseCode.NotFound, "User not found!");
            }
            else
            {
                if (_passwordHashingService.Verify(password, user.HashedPassword))
                {
                    return ResponseModel<User>.GetSuccess(user);
                }
                else
                {
                    return ResponseModel<User>.GetFail(ResponseCode.WrongPassword, "Incorrect password!");
                }
            }
        }
        public async Task<ResponseModel> RemoveAsync(User user) => await _repo.RemoveAsync(user);
        public async Task<ResponseModel> RemoveAsync(int userId)
        {
            var res = GetById(userId);
            if (res.Result is not null)
            {
                await _repo.RemoveAsync(res.Result);
                return ResponseModel.GetSuccess();
            }
            else return ResponseModel.GetFail(ResponseCode.NotFound, "User not found!");
        }
        public async Task<ResponseModel> RemoveAsync(string email)
        {
            var res = GetByEmail(email);
            if (res.Result is not null)
            {
                await _repo.RemoveAsync(res.Result);
                return ResponseModel.GetSuccess();
            }
            else return ResponseModel.GetFail(ResponseCode.NotFound, "User not found!");
        }
        public async Task<ResponseModel> UpdateAsync(Func<User, bool> predicate, Action<User> action) => await _repo.UpdateAsync(predicate, action);
        public async Task<ResponseModel> UpdateAsync(int id, Action<User> action) => await UpdateAsync(x => x.Id == id, action);
        public async Task<ResponseModel> UpdateAsync(string email, Action<User> action) => await UpdateAsync(x => x.Email == email, action);
        public async Task<ResponseModel> AddAsync(User user) => await _repo.AddAsync(user);

    }
}
