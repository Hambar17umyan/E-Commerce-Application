using E_Commerce.API.Data.Repositories;
using E_Commerce.API.Models.DomainModels;
using E_Commerce.API.Models.RequestModels;
using E_Commerce.API.Services;
using E_Commerce.API.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace E_Commerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private RegistrationModelValidator _registrationModelValidator;
        private UserDataRepository _userDataRepository;
        private PasswordHashingService _passwordHasher;
        private RoleManagementService _roleManager;
        public AccountController(RegistrationModelValidator registrationModelValidator, UserDataRepository userDataRepository, PasswordHashingService passwordHasher, RoleManagementService roleManager)
        {
            _registrationModelValidator = registrationModelValidator;
            _userDataRepository = userDataRepository;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
        }

        //#pragma warning disable CS8618 
        //        public AccountController() { }
        //#pragma warning restore CS8618 

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
    }
}
