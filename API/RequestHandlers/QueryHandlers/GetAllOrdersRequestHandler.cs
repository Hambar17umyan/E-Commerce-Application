using API.Models.Domain.Concrete;
using API.Models.Request.Queries;
using API.Models.Response.Output;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.DataServices;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.QueryHandlers
{
    public class GetAllOrdersRequestHandler : IRequestHandler<GetAllOrdersRequestModel, Result<IEnumerable<OrderOutputModel>>>
    {
        private IOrderDataService _orderDataService;
        private IMapper _mapper;

        public GetAllOrdersRequestHandler(IOrderDataService orderDataService, IMapper mapper)
        {
            _orderDataService = orderDataService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<OrderOutputModel>>> Handle(GetAllOrdersRequestModel request, CancellationToken cancellationToken)
        {
            var res = _orderDataService.GetAll();
            if (res.IsSuccess)
            {
                return Result.Ok(
                        _mapper.Map<IEnumerable<Order>, IEnumerable<OrderOutputModel>>(res.Value));
            }
            return Result.Fail(res.Errors);
        }
    }
}
