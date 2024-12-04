using API.Models.Domain;
using API.Models.Request;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers
{
    public class GetAllOrdersRequestHandler : IRequestHandler<GetAllOrdersRequestModel, Result<IEnumerable<Order>>>
    {
        private IOrderDataService _orderDataService;

        public GetAllOrdersRequestHandler(IOrderDataService orderDataService)
        {
            _orderDataService = orderDataService;
        }

        public Task<Result<IEnumerable<Order>>> Handle(GetAllOrdersRequestModel request, CancellationToken cancellationToken)
        {
            var res = _orderDataService.GetAll();
            return Task.FromResult(res);
        }
    }
}
