using API.Models.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EntityControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(policy: "AdminPolicy")]
    public class UsersController : ControllerBase
    {
        #region Services
        private IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion
        #region Get
        [HttpGet]
        [Route("data/")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] GetAllUsersRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Errors);
        }
        #endregion 
    }
}
