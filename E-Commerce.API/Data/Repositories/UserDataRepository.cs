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

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public IQueryable<User> GetAllAsQueryable()
        {
            return _context.Users;
        }
        public async Task<ResponseModel<User>> GetByAsync(Func<User, bool> predicate)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => predicate(x));
            if (user == null)
            {
                return ResponseModel<User>.GetFail(ResponseCode.NotFound, "User not found!");
            }
            else
            {
                return ResponseModel<User>.GetSuccess(user);
            }
        }
        public async Task<ResponseModel> RemoveAsync(int id)
        {
            var resp = await GetByAsync(x=>x.Id == id);
            var user = resp.Result;
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return ResponseModel.GetSuccess();
            }
            else
            {
                return ResponseModel.GetFail(resp.Code, resp.Message);
            }
        }
        public async Task<ResponseModel> UpdateAsync(int id, Action<User> action)
        {
            var resp = await GetByAsync(x => x.Id == id);
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
