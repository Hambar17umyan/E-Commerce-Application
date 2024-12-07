using API.Models.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PublicControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserInterfaceController : ControllerBase
    {
        #region Services
        private IMediator _mediator;

        public UserInterfaceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get

        [HttpGet]
        public async Task<IActionResult> GetCartAsync(GetCartRequestModel request)
        {
            var res = await _mediator.Send(request);
            if(res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return BadRequest(res.Errors);
        }

        //[HttpGet]
        //public IActionResult GetOrderHistory(GetOrderHistoryRequestModel request)
        //{

        //}


        #endregion
    }
}
