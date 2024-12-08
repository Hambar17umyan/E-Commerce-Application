using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Queries;
using API.Models.Response.Output;
using API.Services.Interfaces.DataServices;
using AutoMapper;
using MediatR;

namespace API.RequestHandlers.QueryHandlers
{
    public class GetOrderHistoryRequestHandler : IRequestHandler<GetOrderHistoryRequestModel, InnerResult<IEnumerable<OrderOutputModel>>>
    {
        private IUserDataService _userDataService;
        private IMapper _mapper;

        public GetOrderHistoryRequestHandler(IUserDataService userDataService, IMapper mapper)
        {
            _userDataService = userDataService;
            _mapper = mapper;
        }

        public async Task<InnerResult<IEnumerable<OrderOutputModel>>> Handle(GetOrderHistoryRequestModel request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(request.User.Claims.First(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti).Value);
            var response = _userDataService.GetById(userId);
            if (response.IsSuccess)
            {
                var orderHistory = response.Value.Orders;
                return InnerResult<IEnumerable<OrderOutputModel>>.Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderOutputModel>>(orderHistory));
            }
            else
            {
                throw new Exception("Lav chi!");
            }
        }
    }
}
