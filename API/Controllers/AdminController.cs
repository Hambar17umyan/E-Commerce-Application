using API.Data.Repositories;
using API.Models.Request;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.DataServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private IMediator Mediator;
        private IUserDataService _userDataService;
        private IRoleDataService _roleDataService;

        public AdminController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet]
        [Route("/data/users")]
        public IActionResult GetAllUsers()
        {
            var res = _userDataService.GetAll();
            if(res.IsSuccess)
                return Ok(res.Value);
            return StatusCode(500);
        }

        [HttpGet]
        [Route("/data/roles")]
        public IActionResult GetAllRoles()
        {
            var res = _roleDataService.GetAll();
            if (res.IsSuccess)
                return Ok(res.Value);
            return StatusCode(500);
        }
    }
}
