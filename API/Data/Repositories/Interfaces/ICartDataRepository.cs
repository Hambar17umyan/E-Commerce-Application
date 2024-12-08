using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;

namespace API.Data.Repositories.Interfaces
{
    public interface ICartDataRepository : IDataRepository<Cart>
    {
        /// <summary>
        /// Adds the number of specified product in cart item or creats a new cart item with specified quantity.
        /// </summary>
        /// <param name="cartId">The id of cart.</param>
        /// <param name="product">The product that needs to be added.</param>
        /// <param name="quantity">The number of products that need to be added.</param>
        /// <returns>A task that represents the asynchronous operation, returning an <see cref="InnerResult"/>.</returns>
        Task<InnerResult> AddToCartAsync(int id, Product product, int quantity);


        /// <summary>
        /// Decreases the number of specified product in cart item or removes the cart item.
        /// </summary>
        /// <param name="cartId">The id of cart.</param>
        /// <param name="product">The product that needs to be added.</param>
        /// <param name="quantity">The number of products that need to be removed. If <c>null</c>, the entire cart item will be removed.</param>
        /// <returns>A task that represents the asynchronous operation, returning an <see cref="InnerResult"/>.</returns>
        Task<InnerResult> RemoveFromCartAsync(int id, Product product, int? quantity = null);
    }
}
