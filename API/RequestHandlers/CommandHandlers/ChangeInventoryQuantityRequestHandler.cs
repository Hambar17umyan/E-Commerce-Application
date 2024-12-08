using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Commands;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.CommandHandlers
{
    public class ChangeInventoryQuantityRequestHandler : IRequestHandler<ChangeInventoryQuantityRequestModel, InnerResult>
    {
        private IInventoryDataService _inventoryDataService;

        public ChangeInventoryQuantityRequestHandler(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
        }

        public async Task<InnerResult> Handle(ChangeInventoryQuantityRequestModel request, CancellationToken cancellationToken)
        {
            Action<Inventory> action = x => { };

            if (request.NewQuantity is not null)
            {
                if (request.NewQuantity < 0)
                    return InnerResult.Fail("The quantity cannot be less then 0.");

                var resp = await _inventoryDataService.UpdateAsync(
                    x => x.Id == request.InventoryId,
                    x => x.Quantity = request.NewQuantity.Value
                    );

                if (resp.IsSuccess)
                {
                    return InnerResult.Ok();
                }

                return InnerResult.Fail(resp.Errors, resp.StatusCode);
            }
            else if (request.AddQuantity is not null)
            {
                return await _inventoryDataService.ReduceQuantityAsync(request.InventoryId, -request.AddQuantity.Value);
            }
            else
            {
                return InnerResult.Ok("Warning! No change was applied.");
            }
        }
    }
}
