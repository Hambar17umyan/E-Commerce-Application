﻿using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Commands;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.CommandHandlers
{
    public class AddProductRequestHandler : IRequestHandler<AddProductRequestModel, InnerResult>
    {
        private IInventoryDataService _inventoryDataService;

        public AddProductRequestHandler(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
        }

        public async Task<InnerResult> Handle(AddProductRequestModel request, CancellationToken cancellationToken)
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

            if (res.IsSuccess)
            {
                return InnerResult.Ok();
            }
            else
            {
                return InnerResult.Fail(res.Errors, res.StatusCode);
            }
        }
    }
}
