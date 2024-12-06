using API.Models.Domain.Concrete;
using FluentResults;
using MediatR;
using System.Security.Claims;

namespace API.Models.Request.Queries
{
    public class GetCartRequestModel : IRequest<Result<Cart>>
    {
        internal ClaimsPrincipal User { get; set; }
    }
}
