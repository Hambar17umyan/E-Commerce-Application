using API.Models.Request.Commands;
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
        [Route("cart/data")]
        public async Task<IActionResult> GetCartAsync([FromQuery] GetCartRequestModel request)
        {
            request.User = User;
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode((int)res.StatusCode, res.Errors);
        }

        [HttpGet]
        [Route("orders/")]
        public async Task<IActionResult> GetOrderHistoryAsync([FromQuery] GetOrderHistoryRequestModel request)
        {
            request.User = User;
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode((int)res.StatusCode, res.Errors);
        }

        #endregion

        #region Post

        [HttpPost]
        [Route("cart/add")]
        public async Task<IActionResult> AddProductToCartAsync(AddProductToCartRequestModel request)
        {
            request.User = User;
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return StatusCode((int)res.StatusCode, res.Errors);
        }

        [HttpPost]
        [Route("cart/remove")]
        public async Task<IActionResult> RemoveProductFromCartAsync(RemoveProductFromCartRequestModel request)
        {
            request.User = User;
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return StatusCode((int)res.StatusCode, res.Errors);
        }

        [HttpPost]
        [Route("orders/new")]
        public async Task<IActionResult> CreateNewOrderAsync(CreateNewOrderRequestModel request)
        {
            request.User = User;
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode((int)res.StatusCode, res.Errors);
        }

        #endregion
    }
}
