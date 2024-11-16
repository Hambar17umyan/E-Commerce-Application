using E_Commerce.API.Data.Db;
using E_Commerce.API.Models.DomainModels;
using E_Commerce.API.Models.DTOs;
using E_Commerce.API.Models.DTOs.Enums;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Data.Repositories
{
    public class UserDataRepository
    {
        public UserDataRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        private ECommerceDbContext _context;

        public async Task<ResponseModel> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return ResponseModel.GetSuccess();
        }
        public IQueryable<User> GetAllAsQueryable()
        {
            return _context.Users;
        }
        public ResponseModel<User> GetBy(Func<User, bool> predicate)
        {
            var user = _context.Users.AsEnumerable<User>().FirstOrDefault(x => predicate(x));
            if (user == null)
            {
                return ResponseModel<User>.GetFail(ResponseCode.NotFound, "User not found!");
            }
            else
            {
                return ResponseModel<User>.GetSuccess(user);
            }
        }
        public async Task<ResponseModel> RemoveAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return ResponseModel.GetSuccess();
        }
        public async Task<ResponseModel> UpdateAsync(Func<User, bool> predicate, Action<User> action)
        {
            var resp = GetBy(predicate);
            if (resp.Result != null)
            {
                action(resp.Result);
                await _context.SaveChangesAsync();
                return ResponseModel.GetSuccess();
            }
            return ResponseModel.GetFail(resp.Code, resp.Message);
        }
    }
}
