using E_Commerce.API.Data.Repositories;
using E_Commerce.API.Models.DomainModels;
using E_Commerce.API.Models.RequestModels;
using E_Commerce.API.Services;
using E_Commerce.API.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using E_Commerce.API.Models.DTOs;

namespace E_Commerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private RegistrationModelValidator _registrationModelValidator;
        private UserDataRepository _userDataRepository;
        private PasswordHashingService _passwordHasher;
        private RoleManagementService _roleManager;
        private PasswordHashingService _passwordHashingService;
        private JwtService _jwtService;
        public AccountController(RegistrationModelValidator registrationModelValidator, UserDataRepository userDataRepository, PasswordHashingService passwordHasher, RoleManagementService roleManager, PasswordHashingService passwordHashingService, IConfiguration config, JwtService jwtService)
        {
            _registrationModelValidator = registrationModelValidator;
            _userDataRepository = userDataRepository;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _passwordHashingService = passwordHashingService;
            _config = config;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> RegisterAsync(RegistrationRequestModel request)
        {
            var validation = await _registrationModelValidator.ValidateAsync(request);
            if (validation.IsValid)
            {
                await _userDataRepository.
                    AddAsync(new(request.FirstName,
                    request.LastName,
                    request.Email,
                    _passwordHasher.Hash(request.Password),
                    new List<Role> { _roleManager.GetAdmin() }));
                return Ok();
            }

            return BadRequest(validation.Errors.Aggregate(new StringBuilder(), (curr, next) => curr.Append(next.ErrorMessage)).ToString());
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginAsync(LoginRequestModel request)
        {
            
        }
    }
}
