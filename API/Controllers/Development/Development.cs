using API.Data.Db;
using API.Models.Domain;
using API.Models.Domain.Concrete;
using API.Services.Concrete.Control;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Development
{
    [ApiController]
    [Route("api/[controller]")]
    public class Development : ControllerBase
    {
        ECommerceDbContext _context;
        IPasswordHashingService _passwordHashingService;
        IRoleDataService _roleDataService;
        IUserDataService _userDataService;

        public Development(ECommerceDbContext context, IPasswordHashingService passwordHashingService, IRoleDataService roleDataService, IUserDataService userDataService)
        {
            _context = context;
            _passwordHashingService = passwordHashingService;
            _roleDataService = roleDataService;
            _userDataService = userDataService;
        }

        [HttpDelete]
        [Route("removedata/")]
        public async Task RemoveDataFromDbAsync()
        {
            _context.Inventories.RemoveRange(_context.Inventories);
            _context.LineItems.RemoveRange(_context.LineItems);
            _context.Orders.RemoveRange(_context.Orders);
            _context.Products.RemoveRange(_context.Products);
            _context.Roles.RemoveRange(_context.Roles);
            _context.Users.RemoveRange(_context.Users);
            _context.CartItems.RemoveRange(_context.CartItems);
            _context.Carts.RemoveRange(_context.Carts);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("AdminCheck/")]
        [Authorize(Roles = "Admin")]
        public IActionResult Beep()
        {
            Console.Beep();
            return Ok();
        }


        [HttpPost]
        [Route("addsuperadmin/")]
        public async Task<IActionResult> AddSuperAdmin()
        {
            var a = new User()
            {
                FirstName = "Sergey",
                LastName = "Hambardzumyan",
                Email = "hambardzumyan7serg@gmail.com",
                PasswordHash = _passwordHashingService.Hash("P@ssw0rd"),
                Roles = new List<Role>()
                {
                    _roleDataService.GetSuperAdmin()
                },
                Cart = new Cart(),
                IsActive = true,
                Orders = new List<Order>()
            };
            return Ok((await _userDataService.AddAsync(a)).IsSuccess);
        }
    }

}
