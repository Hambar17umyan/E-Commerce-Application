using API.Data.Repositories.Concrete;
using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Services.Concrete.Control;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using FluentResults;
using System.Net;

namespace API.Services.Concrete.DataServices
{
    public sealed class UserDataService : DataService<User>, IUserDataService
    {
        private IPasswordHashingService _passwordHashingService;
        private ICartDataRepository _cartDataRepository;
        private IProductDataRepository _productDataRepository;

        public UserDataService(IUserDataRepository repo, IPasswordHashingService passwordHashingService, ICartDataRepository cartDataRepository, IProductDataRepository productDataRepository) : base(repo)
        {
            _passwordHashingService = passwordHashingService;
            _cartDataRepository = cartDataRepository;
            _productDataRepository = productDataRepository;
        }
        public InnerResult<User> GetByEmail(string email) => GetBy(x => x.Email == email);
        public InnerResult<User> Authenticate(string email, string password)
        {
            var resp = GetByEmail(email);

            if (resp.IsSuccess)
            {
                var user = resp.Value;
                if (_passwordHashingService.Verify(password, user.PasswordHash))
                {
                    return InnerResult<User>.Ok(user);
                }
                else
                {
                    return InnerResult<User>.Fail("Incorrect password!", HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return InnerResult<User>.Fail(resp.Errors, resp.StatusCode);
            }
        }
        public async Task<InnerResult> RemoveAsync(string email)
        {
            var res = GetByEmail(email);
            if (res.IsSuccess)
            {
                await _repo.RemoveAsync(res.Value);
                return InnerResult.Ok();
            }
            else return InnerResult.Fail(res.Errors, res.StatusCode);
        }
        public async Task<InnerResult> UpdateAsync(int id, Action<User> action) => await UpdateAsync(x => x.Id == id, action);
        public async Task<InnerResult> UpdateAsync(string email, Action<User> action) => await UpdateAsync(x => x.Email == email, action);


        public async Task<InnerResult> AddToCartAsync(int userId, int productId, int quantity)
        {
            //Edge case
            var productResp = _productDataRepository.GetById(productId);

            if (productResp.IsSuccess)
                return await AddToCartAsync(userId, productResp.Value, quantity);
            else
                return InnerResult.Fail(productResp.Errors, productResp.StatusCode);
        }
        public async Task<InnerResult> AddToCartAsync(int id, Product product, int quantity)
        {
            var userResp = GetById(id);
            if (userResp.IsFailed)
                return InnerResult.Fail(userResp.Errors, userResp.StatusCode);

            var user = userResp.Value;
            var cart = user.Cart;

            return await _cartDataRepository.AddToCartAsync(cart.Id, product, quantity);
        }

        public async Task<InnerResult> RemoveFromCartAsync(int userId, int productId, int? quantity)
        {
            //Edge case
            var productResp = _productDataRepository.GetById(productId);

            if (productResp.IsSuccess)
                return await RemoveFromCartAsync(userId, productResp.Value, quantity);
            else
                return InnerResult.Fail(productResp.Errors, productResp.StatusCode);
        }

        public async Task<InnerResult> RemoveFromCartAsync(int userId, Product product, int? quantity)
        {
            var userResp = GetById(userId);
            if (userResp.IsFailed)
                return InnerResult.Fail(userResp.Errors, userResp.StatusCode);

            var user = userResp.Value;
            var cart = user.Cart;

            return await _cartDataRepository.RemoveFromCartAsync(cart.Id, product, quantity);
        }
    }
}
