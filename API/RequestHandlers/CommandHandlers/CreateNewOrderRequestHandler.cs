using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Commands;
using API.Models.Response.Output;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using AutoMapper;
using MediatR;

namespace API.RequestHandlers.CommandHandlers
{
    public class CreateNewOrderRequestHandler : IRequestHandler<CreateNewOrderRequestModel, InnerResult<OrderOutputModel>>
    {
        private IOrderCreationService _orderCreationService;
        private IUserDataService _userDataService;
        private IUserRetrieverService _userRetrieverService;
        private IMapper _mapper;

        public CreateNewOrderRequestHandler(IOrderCreationService orderCreationService, IUserDataService userDataService, IUserRetrieverService userRetrieverService, IMapper mapper)
        {
            _orderCreationService = orderCreationService;
            _userDataService = userDataService;
            _userRetrieverService = userRetrieverService;
            _mapper = mapper;
        }

        public async Task<InnerResult<OrderOutputModel>> Handle(CreateNewOrderRequestModel request, CancellationToken cancellationToken)
        {
            var userResp = _userRetrieverService.GetUser(request.User);

            if (userResp.IsFailed)
                return InnerResult<OrderOutputModel>.Fail(userResp.Errors, userResp.StatusCode);

            var res = await _orderCreationService.CreateOrderAsync(userResp.Value);

            if (res.IsFailed)
                return InnerResult<OrderOutputModel>.Fail(res.Errors, res.StatusCode);

            return InnerResult<OrderOutputModel>.Ok(_mapper.Map<Order, OrderOutputModel>(res.Value));
        }
    }
}
