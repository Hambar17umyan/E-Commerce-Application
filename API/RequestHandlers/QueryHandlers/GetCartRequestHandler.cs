using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Queries;
using API.Models.Response.Output;
using API.Services.Interfaces.Control;
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
        private IUserRetrieverService _userRetrieverService;
        public GetCartRequestHandler(IUserDataService userDataService, IMapper mapper, IUserRetrieverService userRetrieverService)
        {
            _userDataService = userDataService;
            _mapper = mapper;
            _userRetrieverService = userRetrieverService;
        }

        public async Task<InnerResult<CartOutputModel>> Handle(GetCartRequestModel request, CancellationToken cancellationToken)
        {
            var response = _userRetrieverService.GetUser(request.User);
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
