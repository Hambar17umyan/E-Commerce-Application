using API.Data.Db;
using API.Models.Domain;
using API.Services.Concrete.Control;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
        //[AllowAnonymous]
       // [Authorize(Roles = "Admin")]
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

        [HttpPost]
        [Route("/AdminCheck")]
        [Authorize(Roles = "Admin")]
        public IActionResult Beep()
        {
            Console.Beep();
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> HelloAsync()
        {
            var a = new User()
            {
                FirstName = "Sergey",
                LastName = "Hambardzumyan",
                Email = "hambardzumyanserg17@gmail.com",
                PasswordHash = _passwordHashingService.Hash("Admin12345678!"),
                Cart = new(),
                Roles = new List<Role>()
                {
                    new Role()
                    {
                        Name = "Admin",
                        Description = "Es sagh karam",
                        Priority = 1
                    }
                },
                Orders = new List<Order>(),
                IsActive = true
            };
            return Ok((await _userDataService.AddAsync(a)).IsSuccess);
        }
    }

}
