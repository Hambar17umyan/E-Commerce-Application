﻿using API.Models.Domain;
using API.Models.Request;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequestModel, Result<IEnumerable<Product>>>
    {
        private IProductDataService _productDataService;

        public GetAllProductsRequestHandler(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public Task<Result<IEnumerable<Product>>> Handle(GetAllProductsRequestModel request, CancellationToken cancellationToken)
        {
            var res = _productDataService.GetAll();
            return Task.FromResult(res);
        }
    }
}
