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
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequestModel, Result<IEnumerable<ProductOutputModel>>>
    {
        private IProductDataService _productDataService;
        private IMapper _mapper;

        public GetAllProductsRequestHandler(IProductDataService productDataService, IMapper mapper)
        {
            _productDataService = productDataService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductOutputModel>>> Handle(GetAllProductsRequestModel request, CancellationToken cancellationToken)
        {
            var res = _productDataService.GetAll();
            if (res.IsSuccess)
            {
                return Result.Ok(
                        _mapper.Map<IEnumerable<Product>, IEnumerable<ProductOutputModel>>(res.Value));
            }
            return Result.Fail(res.Errors);
        }
    }
}
