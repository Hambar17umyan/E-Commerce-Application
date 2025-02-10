using API.Data.Db;
using API.Models.Control.Email;
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
        private IEmailService _emailService;
        private IPdfService _pdfService;
        private ECommerceDbContext _context;

        public OrderCreationService(IInventoryDataService inventoryDataService, IProductDataService productDataService, IOrderDataService orderDataService, ECommerceDbContext context, IEmailService emailService, IPdfService pdfService)
        {
            _inventoryDataService = inventoryDataService;
            _productDataService = productDataService;
            _orderDataService = orderDataService;
            _context = context;
            _emailService = emailService;
            _pdfService = pdfService;
        }

        public async Task<InnerResult<Order>> CreateOrderAsync(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            var cart = user.Cart;

            if (!cart.Items.Any())
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
            if (res1.IsFailed)
            {
                transaction.Rollback();
                return InnerResult<Order>.Fail(res1.Errors, res1.StatusCode);
            }

            order.CreationDateTime = DateTime.Now;

            cart.Items.Clear();
            user.Orders.Add(order);

            var email = new EmailModel()
            {
                Email = user.Email,
                Attachments = new List<EmailAttachment>()
                {
                    new EmailAttachment()
                    {
                        Data = _pdfService.GenerateOrderPdf(order),
                        Name = $"Order_{order.Id}_Documentation.pdf"
                    },
                },
                Subject = $"Order N{order.Id} Confirmation",
                BodyText =
                $"<p><b>Dear {user.FirstName} {user.LastName},</b></p>" +
                $"<p>Thank you for your recent order! We are excited to serve you and provide you with the best experience possible.</p><br>" +
                $"<p>Your order has been successfully created and is being processed. Below, you can find the details of your order. Please check the attached PDF document for a summary of your purchase.</p><br>" +
                $"<p>If you have any questions or require assistance, feel free to reply to this email or contact our support team.</p><br>" +
                $"<b><p>Best regards,</p><p>The Debed Team</p></b>"
            };

            await _emailService.SendEmailAsync(email);

            await _context.SaveChangesAsync();

            transaction.Commit();

            return order;
        }
    }
}
