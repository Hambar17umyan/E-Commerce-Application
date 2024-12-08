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

        /// <summary>
        /// Adds a specified number of specified product to user's cart.
        /// </summary>
        /// <param name="userId">The id of user.</param>
        /// <param name="product">The product.</param>
        /// <param name="quantity">The number of products.</param>
        /// <returns>A task that represents the asynchronous operation, returning an <see cref="InnerResult"/>.</returns>
        public Task<InnerResult> AddToCartAsync(int id, Product product, int quantity);

        /// <summary>
        /// Adds a specified number of specified product to user's cart.
        /// </summary>
        /// <param name="userId">The id of user.</param>
        /// <param name="productId">The id of product.</param>
        /// <param name="quantity">The number of products.</param>
        /// <returns>A task that represents the asynchronous operation, returning an <see cref="InnerResult"/>.</returns>
        public Task<InnerResult> AddToCartAsync(int userId, int productId, int quantity);

        /// <summary>
        /// Removes a specified number of specified product to user's cart or the whole cart item.
        /// </summary>
        /// <param name="userId">The id of user.</param>
        /// <param name="productId">The id of product.</param>
        /// <param name="quantity">The number of products that need to be removed. If <c>null</c>, the entire cart item will be removed.</param>
        /// <returns>A task that represents the asynchronous operation, returning an <see cref="InnerResult"/>.</returns>
        public Task<InnerResult> RemoveFromCartAsync(int userId, int productId, int? quantity);

        /// <summary>
        /// Removes a specified number of specified product to user's cart or the whole cart item.
        /// </summary>
        /// <param name="userId">The id of user.</param>
        /// <param name="product">The product.</param>
        /// <param name="quantity">The number of products that need to be removed. If <c>null</c>, the entire cart item will be removed.</param>
        /// <returns>A task that represents the asynchronous operation, returning an <see cref="InnerResult"/>.</returns>
        public Task<InnerResult> RemoveFromCartAsync(int userId, Product product, int? quantity);
    }
}
