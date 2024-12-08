using API.Data.Db;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using System.Net;

namespace API.Services.Concrete.Control
{
    public class OrderCreationService : IOrderCreationService
    {
        private IInventoryDataService _inventoryDataService;
        private IProductDataService _productDataService;
        private IOrderDataService _orderDataService;
        private ECommerceDbContext _context;

        public OrderCreationService(IInventoryDataService inventoryDataService, IProductDataService productDataService, IOrderDataService orderDataService, ECommerceDbContext context)
        {
            _inventoryDataService = inventoryDataService;
            _productDataService = productDataService;
            _orderDataService = orderDataService;
            _context = context;
        }

        public async Task<InnerResult<Order>> CreateOrderAsync(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            var cart = user.Cart;

            if(!cart.Items.Any())
            {
                transaction.Rollback();
                return InnerResult<Order>.Fail("The cart is empty!", HttpStatusCode.BadRequest);
            }

            var order = new Order();
            order.LineItems = new List<LineItem>();

            foreach (var item in cart.Items)
            {
                var inventoryResp = _inventoryDataService.GetBy(x => x.Product == item.Product);
                if (inventoryResp.IsFailed)
                {
                    transaction.Rollback();
                    return InnerResult<Order>.Fail("Something went wrong!", HttpStatusCode.InternalServerError);
                }

                var inventory = inventoryResp.Value;
                var res = await _inventoryDataService.ReduceQuantityAsync(inventory.Id, item.Quantity);
                if (res.IsFailed)
                {
                    transaction.Rollback();
                    return InnerResult<Order>.Fail($"There are no enough products of id {item.Product.Id} in our shop!", HttpStatusCode.Conflict);
                }

                order.LineItems.Add(item);
            }

            var res1 = await _orderDataService.AddAsync(order);
            if(res1.IsFailed)
            {
                transaction.Rollback();
                return InnerResult<Order>.Fail(res1.Errors, res1.StatusCode);
            }

            cart.Items.Clear();
            user.Orders.Add(order);

            await _context.SaveChangesAsync();

            transaction.Commit();

            return order;
        }
    }
}
