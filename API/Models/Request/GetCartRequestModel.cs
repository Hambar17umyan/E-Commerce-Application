using API.Models.Domain;
using FluentResults;
using MediatR;
using System.Security.Claims;

namespace API.Models.Request
{
    public class GetCartRequestModel : IRequest<Result<Cart>>
    {
        internal ClaimsPrincipal User { get; set; }
    }
}
