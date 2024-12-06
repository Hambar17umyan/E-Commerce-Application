using API.Models.Domain.Concrete;
using API.Models.Request.Queries;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.QueryHandlers
{
    public class GetCartRequestHandler : IRequestHandler<GetCartRequestModel, Result<Cart>>
    {
        private IUserDataService _userDataService;

        public GetCartRequestHandler(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public Task<Result<Cart>> Handle(GetCartRequestModel request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(request.User.Claims.First(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti).Value);
            var response = _userDataService.GetById(userId);
            if (response.IsSuccess)
            {
                var cart = response.Value.Cart;
                return Task.FromResult(Result.Ok(cart));
            }
            else
            {
                throw new Exception("Du tqir yegho!");
            }
        }
    }
}
