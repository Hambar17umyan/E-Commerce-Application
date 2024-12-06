﻿using API.Models.Domain;
using API.Models.Request;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers
{
    public class ChangeInventoryQuantityManualRequestHandler : IRequestHandler<ChangeInventoryQuantityManualRequestModel, Result>
    {
        private IInventoryDataService _inventoryDataService;

        public ChangeInventoryQuantityManualRequestHandler(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
        }

        public async Task<Result> Handle(ChangeInventoryQuantityManualRequestModel request, CancellationToken cancellationToken)
        {
            Action<Inventory> changeFactory = x => { };

            if (request.NewQuantity is not null)
                changeFactory += x => x.Quantity = request.NewQuantity.Value;
            else if (request.AddQuantity is not null)
                changeFactory += x => x.Quantity += request.AddQuantity.Value;
            else
                return Result.Ok().WithSuccess("Warning! No change was applied.");

            var resp = await _inventoryDataService.UpdateAsync(x => x.Id == request.InventoryId, changeFactory);

            if (resp.IsSuccess)
            {
                return Result.Ok();
            }

            return Result.Fail(resp.Errors);
        }
    }
}
