using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using FluentResults;
using MediatR;
using System.Security.Claims;

namespace API.Models.Request.Queries
{
    public class GetCartRequestModel : IRequest<InnerResult<CartOutputModel>>
    {
        internal ClaimsPrincipal User { get; set; }
    }
}
