using API.Data.Repositories;
using API.Models.Request;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.DataServices;
using Azure.Core;
using FluentResults;
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
        private IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/users/data")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery]GetAllUsersRequestModel request)
        {
            var res = await _mediator.Send(request);
            if(res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Errors);
        }

        [HttpGet]
        [Route("/roles/data")]
        public async Task<IActionResult> GetAllRolesAsync([FromQuery] GetAllRolesRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Errors);
        }

        [HttpGet]
        [Route("/orders/data")]
        public async Task<IActionResult> GetAllOrdersAsync([FromQuery] GetAllOrdersRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Errors);
        }

        [HttpGet]
        [Route("/products/data")]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProductsRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return StatusCode(500, res.Errors);
        }

        [HttpPut]
        [Route("/users/setadmin")]
        public async Task<IActionResult> SetAdminAsync(SetAdminRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, res.Errors);
        }

    }
}
