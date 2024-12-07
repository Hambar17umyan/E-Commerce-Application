using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Request.Queries;
using API.Models.Response.Output;
using API.Services.Interfaces.DataServices;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.QueryHandlers
{
    public class GetAllInventoriesRequestHandler : IRequestHandler<GetAllInventoriesRequestModel, InnerResult<IEnumerable<InventoryOutputModel>>>
    {
        private IInventoryDataService _inventoryDataService;
        private IMapper _mapper;
        public GetAllInventoriesRequestHandler(IInventoryDataService inventoryDataService, IMapper mapper)
        {
            _inventoryDataService = inventoryDataService;
            _mapper = mapper;
        }

        public async Task<InnerResult<IEnumerable<InventoryOutputModel>>> Handle(GetAllInventoriesRequestModel request, CancellationToken cancellationToken)
        {
            var res = _inventoryDataService.GetAll();
            if (res.IsSuccess)
            {
                var ret = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryOutputModel>>(res.Value);
                return InnerResult<IEnumerable<InventoryOutputModel>>.Ok(ret);
            }
            return InnerResult<IEnumerable<InventoryOutputModel>>.Fail(res.Errors, res.StatusCode);
        }
    }
}
