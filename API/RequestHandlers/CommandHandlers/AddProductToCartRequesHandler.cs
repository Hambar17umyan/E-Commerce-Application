using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Commands;
using API.Models.Response.Output;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using AutoMapper;
using Azure;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace API.RequestHandlers.CommandHandlers
{
    public class AddProductToCartRequestHandler : IRequestHandler<AddProductToCartRequestModel, InnerResult>
    {
        private IUserDataService _userDataService;
        private IUserRetrieverService _userRetrieverService;
        private ICartControllerService _cartControllerService;
        private IProductDataService _productDataService;
        public AddProductToCartRequestHandler(IUserDataService userDataService, IUserRetrieverService userRetrieverService, ICartControllerService cartControllerService, IProductDataService productDataService)
        {
            _userDataService = userDataService;
            _userRetrieverService = userRetrieverService;
            _cartControllerService = cartControllerService;
            _productDataService = productDataService;
        }

        public async Task<InnerResult> Handle(AddProductToCartRequestModel request, CancellationToken cancellationToken)
        {
            var resp1 = _userRetrieverService.GetUserId(request.User);
            if (resp1.IsFailed)
                return InnerResult.Fail(resp1.Errors, resp1.StatusCode);

            var id = resp1.Value;

            var resp2 = await _userDataService.AddToCartAsync(id, request.ProductId, request.Quantity);
            if (resp2.IsFailed)
                return resp2;
            return InnerResult.Ok();
        }
    }
}
