using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Commands;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.CommandHandlers
{
    public class ChangeInventoryQuantityManualRequestHandler : IRequestHandler<ChangeInventoryQuantityManualRequestModel, InnerResult>
    {
        private IInventoryDataService _inventoryDataService;

        public ChangeInventoryQuantityManualRequestHandler(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
        }

        public async Task<InnerResult> Handle(ChangeInventoryQuantityManualRequestModel request, CancellationToken cancellationToken)
        {
            Action<Inventory> changeFactory = x => { };

            if (request.NewQuantity is not null)
                changeFactory += x => x.Quantity = request.NewQuantity.Value;
            else if (request.AddQuantity is not null)
                changeFactory += x => x.Quantity += request.AddQuantity.Value;
            else
                return InnerResult.Ok("Warning! No change was applied.");

            var resp = await _inventoryDataService.UpdateAsync(x => x.Id == request.InventoryId, changeFactory);

            if (resp.IsSuccess)
            {
                return InnerResult.Ok();
            }

            return InnerResult.Fail(resp.Errors, resp.StatusCode);
        }
    }
}
