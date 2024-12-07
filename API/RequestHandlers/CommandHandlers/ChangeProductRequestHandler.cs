using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Commands;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.CommandHandlers
{
    public class ChangeProductRequestHandler : IRequestHandler<ChangeProductRequestModel, InnerResult>
    {
        private IProductDataService _productDataService;

        public ChangeProductRequestHandler(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public async Task<InnerResult> Handle(ChangeProductRequestModel request, CancellationToken cancellationToken)
        {
            Action<Product> changeFactory = x => { };

            if (request.NewName is not null)
                changeFactory += x => x.Name = request.NewName;

            if (request.NewPrice is not null)
                changeFactory += x => x.Price = request.NewPrice.Value;

            if (request.NewDescription is not null)
                changeFactory += x => x.Description = request.NewDescription;

            var resp = await _productDataService.UpdateAsync(x => x.Id == request.ProductId, changeFactory);

            if (resp.IsSuccess)
            {
                return InnerResult.Ok();
            }

            return InnerResult.Fail(resp.Errors, resp.StatusCode);
        }
    }
}
