using API.Models.Domain;
using API.Models.Request;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers
{
    public class GetAllInventoriesRequestHandler : IRequestHandler<GetAllInventoriesRequestModel, Result<IEnumerable<Inventory>>>
    {
        private IInventoryDataService _inventoryDataService;

        public GetAllInventoriesRequestHandler(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
        }

        public Task<Result<IEnumerable<Inventory>>> Handle(GetAllInventoriesRequestModel request, CancellationToken cancellationToken)
        {
            var res = _inventoryDataService.GetAll();
            return Task.FromResult(res);
        }
    }
}
