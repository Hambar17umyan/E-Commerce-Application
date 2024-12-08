using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using MediatR;
using System.Security.Claims;

namespace API.Models.Request.Commands
{
    public class CreateNewOrderRequestModel : IRequest<InnerResult<OrderOutputModel>>
    {
        internal ClaimsPrincipal User { get; set; }
    }
}
