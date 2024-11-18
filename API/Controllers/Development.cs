using API.Data.Db;
using API.Models.Domain;
using API.Services.Control;
using API.Services.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    public class Development : ControllerBase
    {
        ECommerceDbContext _context;
        PasswordHashingService _passwordHashingService;
        RoleDataService _roleDataService;
        UserDataService _userDataService;

        public Development(ECommerceDbContext context, PasswordHashingService passwordHashingService, RoleDataService roleDataService, UserDataService userDataService)
        {
            _context = context;
            _passwordHashingService = passwordHashingService;
            _roleDataService = roleDataService;
            _userDataService = userDataService;
        }

        [HttpDelete]
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


        //[HttpPost]
        //public async Task<IActionResult> HelloAsync()
        //{
        //    var a = new User()
        //    {
        //        FirstName = "Sergey",
        //        LastName = "Hambardzumyan",
        //        Email = "hambardzumyanserg17@gmail.com",
        //        PasswordHash = _passwordHashingService.Hash("Admin12345678!"),
        //        Cart = new(),
        //        Roles = new List<Role>()
        //        {
        //            _roleDataService.GetAdmin()
        //        },
        //        Orders = new List<Order>(),
        //        IsActive = true
        //    };
        //    return Ok((await _userDataService.AddAsync(a)).IsSuccess);
        //}
    }

}
