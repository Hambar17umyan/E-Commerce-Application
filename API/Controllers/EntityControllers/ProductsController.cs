using API.Models.Request.Commands;
using API.Models.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EntityControllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        #region Services
        private IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion
        #region Get
        [HttpGet]
        [Authorize(policy: "AdminPolicy")]
        [Route("data/")]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProductsRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Errors);
        }
        #endregion
        #region Post
        [HttpPost]
        [Route("add")]
        [Authorize("AdminPolicy")]
        public async Task<IActionResult> AddProductAsync(AddProductRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(500, res.Errors);
        }
        #endregion
        #region Put
        [HttpPut]
        [Route("change")]
        [Authorize("AdminPolicy")]
        public async Task<IActionResult> ChangeProductAsync(ChangeProductRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(res.Errors.Select(x => x.Message));
        }
        #endregion
    }
}
