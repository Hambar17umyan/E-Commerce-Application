using E_Commerce.API.Data.Repositories;
using E_Commerce.API.Models.DomainModels;
using E_Commerce.API.Models.RequestModels;
using E_Commerce.API.Services;
using E_Commerce.API.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using E_Commerce.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private RegistrationModelValidator _registrationModelValidator;
        private PasswordHashingService _passwordHasher;
        private RoleManagementService _roleManager;
        private PasswordHashingService _passwordHashingService;
        private JwtService _jwtService;
        private UserDataService _userDataService;
        private LoginModelValidator _loginModelValidator;
        public AccountController(RegistrationModelValidator registrationModelValidator, PasswordHashingService passwordHasher, RoleManagementService roleManager, PasswordHashingService passwordHashingService, IConfiguration config, JwtService jwtService, UserDataService userDataService, LoginModelValidator loginModelValidator)
        {
            _registrationModelValidator = registrationModelValidator;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _passwordHashingService = passwordHashingService;
            _config = config;
            _jwtService = jwtService;
            _userDataService = userDataService;
            _loginModelValidator = loginModelValidator;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> RegisterAsync(RegistrationRequestModel request)
        {
            var validation = await _registrationModelValidator.ValidateAsync(request);
            if (validation.IsValid)
            {
                await _userDataService.AddAsync(new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    HashedPassword = _passwordHasher.Hash(request.Password),
                    IsActive = true,
                    Roles = new List<Role>() { _roleManager.GetCustomer() }
                });
                return Ok();
            }

            return BadRequest(validation.Errors.Aggregate(new StringBuilder(), (curr, next) => curr.Append(next.ErrorMessage)).ToString());
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginAsync(LoginRequestModel request)
        {
            var validation = await _loginModelValidator.ValidateAsync(request);
            if (validation.IsValid)
            {
                var response = _userDataService.Authenticate(request.Email, request.Password);
                if(response.Result is not null)
                {
                    var token = _jwtService.Generate(response.Result);
                    HttpContext.Response.Headers.Append("Auth", token);
                    return Ok(token);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }

            return BadRequest(validation.Errors.Aggregate(new StringBuilder(), (curr, next) => curr.Append(next.ErrorMessage)).ToString());
        }
    }
}