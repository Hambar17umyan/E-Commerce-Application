using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using System.Net;

namespace API.Data.Repositories.Concrete
{
    public class CartDataRepository : DataRepository<Cart>, ICartDataRepository
    {
        public CartDataRepository(ECommerceDbContext context) : base(context, context.Carts) { }

        public async Task<InnerResult> AddToCartAsync(int cartId, Product product, int quantity)
        {
            var cartResp = GetById(cartId);

            //Edge Case
            if (cartResp.IsFailed)
            {
                return InnerResult.Fail(cartResp.Errors, cartResp.StatusCode);
            }

            var cart = cartResp.Value;
            var cartItems = cart.Items;
            foreach (var item in cartItems)
            {
                if (item.Product == product)
                {
                    item.Quantity += quantity;
                    await _context.SaveChangesAsync();
                    return InnerResult.Ok();
                }
            }

            var newItem = new CartItem()
            {
                Product = product,
                CartId = cart.Id,
                Quantity = quantity,
            };
            cart.Items.Add(newItem);
            await _context.SaveChangesAsync();
            return InnerResult.Ok();
        }

        public async Task<InnerResult> RemoveFromCartAsync(int cartId, Product product, int? quantity = null)
        {
            var cartResp = GetById(cartId);

            //Edge Case
            if (cartResp.IsFailed)
            {
                return InnerResult.Fail(cartResp.Errors, cartResp.StatusCode);
            }

            var cart = cartResp.Value;
            if (quantity is not null) //Removes specified number of products.
            {
                foreach (var item in cart.Items)
                {
                    if (item.Product == product)
                    {
                        if (item.Quantity > quantity)
                        {
                            item.Quantity -= quantity.Value;
                            await _context.SaveChangesAsync();
                            return InnerResult.Ok();
                        }
                        else if (item.Quantity == quantity)
                        {
                            var list = cart.Items.
                                Where(x => x.Product != product)
                                .ToList();

                            if (list.Count == cart.Items.Count)
                                return InnerResult.Fail("Cart doesn't contain that product!", HttpStatusCode.BadRequest);

                            cart.Items = list;
                            await _context.SaveChangesAsync();
                            return InnerResult.Ok();
                        }
                        else
                        {
                            return InnerResult.Fail("The number of that product is smaller then specified!", HttpStatusCode.BadRequest);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return InnerResult.Fail("Cart doesn't contain that product!", HttpStatusCode.BadRequest);
            }
            else //Removes the CartItem
            {
                var list = cart.Items.
                    Where(x => x.Product != product)
                    .ToList();

                if (list.Count == cart.Items.Count)
                    return InnerResult.Fail("Cart doesn't contain that product!", HttpStatusCode.BadRequest);

                cart.Items = list;
                await _context.SaveChangesAsync();
                return InnerResult.Ok();
            }
        }
    }
}
