using E_Commerce.API.Data.Repositories;
using E_Commerce.API.Models.DomainModels;
using E_Commerce.API.Models.DTOs;

namespace E_Commerce.API.Services
{
    public class UserDataService
    {
        private UserDataRepository _repo;

        public UserDataService(UserDataRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseModel<User>> GetByIdAsync(int id)
        {
            return await _repo.GetByAsync(x => x.Id == id);
        }
        public async Task<ResponseModel<User>> GetByEmailAsync(int email)
        {
            return await _repo.GetByAsync(x => x.Email == email);
        }
        public ResponseModel<User> CheckIfExists(int id)
        {

        }
    }
}
