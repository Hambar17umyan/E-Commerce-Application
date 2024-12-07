using API.Models.Control.ResultModels;
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
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequestModel, InnerResult<IEnumerable<ProductOutputModel>>>
    {
        private IProductDataService _productDataService;
        private IMapper _mapper;

        public GetAllProductsRequestHandler(IProductDataService productDataService, IMapper mapper)
        {
            _productDataService = productDataService;
            _mapper = mapper;
        }

        public async Task<InnerResult<IEnumerable<ProductOutputModel>>> Handle(GetAllProductsRequestModel request, CancellationToken cancellationToken)
        {
            var res = _productDataService.GetAll();
            if (res.IsSuccess)
            {
                return InnerResult<IEnumerable<ProductOutputModel>>.Ok(
                        _mapper.Map<IEnumerable<Product>, IEnumerable<ProductOutputModel>>(res.Value));
            }
            return InnerResult<IEnumerable<ProductOutputModel>>.Fail(res.Errors, res.StatusCode);
        }
    }
}
