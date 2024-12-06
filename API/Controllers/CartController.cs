using API.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartAsync(GetCartRequestModel request)
        {
            request.User = User;
            var resp = await _mediator.Send(request);

            if(resp.IsSuccess)
            {
                return Ok(resp);
            }
            return BadRequest(resp.Errors);
        }
    }
}
