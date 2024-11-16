using Microsoft.AspNetCore.Mvc;
using E_Commerce.API.Data.Db;

namespace E_Commerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Development : ControllerBase
    {
        ECommerceDbContext _context;

        public Development(ECommerceDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task RemoveDataFromDbAsync()
        {
            _context.Inventories.RemoveRange(_context.Inventories);
            _context.LineItems.RemoveRange(_context.LineItems);
            _context.Orders.RemoveRange(_context.Orders);
            _context.Products.RemoveRange(_context.Products);
            _context.Roles.RemoveRange(_context.Roles);
            _context.Users.RemoveRange(_context.Users);
            await _context.SaveChangesAsync();
        }
    }
}