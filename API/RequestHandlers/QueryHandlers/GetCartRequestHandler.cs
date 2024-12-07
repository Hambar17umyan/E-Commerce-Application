using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Queries;
using API.Models.Response.Output;
using API.Services.Interfaces.DataServices;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.QueryHandlers
{
    public class GetCartRequestHandler : IRequestHandler<GetCartRequestModel, InnerResult<CartOutputModel>>
    {
        private IUserDataService _userDataService;
        private IMapper _mapper;

        public GetCartRequestHandler(IUserDataService userDataService, IMapper mapper)
        {
            _userDataService = userDataService;
            _mapper = mapper;
        }

        public async Task<InnerResult<CartOutputModel>> Handle(GetCartRequestModel request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(request.User.Claims.First(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti).Value);
            var response = _userDataService.GetById(userId);
            if (response.IsSuccess)
            {
                var cart = response.Value.Cart;
                return InnerResult<CartOutputModel>.Ok(_mapper.Map<Cart, CartOutputModel>(cart));
            }
            else
            {
                throw new Exception("Lav chi!");
            }
        }
    }
}
