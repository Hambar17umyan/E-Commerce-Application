using API.Models.Domain;
using API.Models.Request;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers
{
    public class AddProductRequestHandler : IRequestHandler<AddProductRequestModel, Result>
    {
        private IInventoryDataService _inventoryDataService;

        public AddProductRequestHandler(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
        }

        public async Task<Result> Handle(AddProductRequestModel request, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
            };
            Inventory inventory = new()
            {
                Product = product,
                Quantity = request.Quantity,
            };

            var res = await _inventoryDataService.AddAsync(inventory);

            if(res.IsSuccess)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail(res.Errors);
            }
        }
    }
}
