using API.Models.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EntityControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(policy: "AdminPolicy")]
    public class OrdersController : ControllerBase
    {
        #region Services
        private IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion
        #region Get
        [HttpGet]
        [Route("data/")]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllOrdersRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Errors.Select(x => x.Message));
        }
        #endregion
    }
}
