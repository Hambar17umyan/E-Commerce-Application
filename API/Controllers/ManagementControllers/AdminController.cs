using API.Data.Repositories;
using API.Models.Request.Commands;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.DataServices;
using Azure.Core;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ManagementControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("AdminPolicy")]
    public class AdminController : ControllerBase
    {
        #region Services
        private IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion
        #region Put
        [HttpPut]
        [Route("setadmin")]
        public async Task<IActionResult> SetAdminAsync(SetAdminRequestModel request)
        {
            var res = await _mediator.Send(request);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return StatusCode(500, res.Errors);
        }

        
        #endregion
    }
}
