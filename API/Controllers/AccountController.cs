﻿using API.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> RegisterAsync(RegistrationRequestModel request)
        {
            var res = await _mediator.Send(request);
            return res.IsSuccess ?
                Ok() :
                BadRequest(res.Errors[0].Message);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginAsync(LoginRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                HttpContext.Response.Headers.Append("Auth", res.Value);
                return Ok(res.Value);
            }
            return BadRequest(res.Errors[0].Message);
        }
    }
}
