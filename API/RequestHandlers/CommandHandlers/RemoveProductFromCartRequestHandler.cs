using API.Models.Control.ResultModels;
using API.Models.Request.Commands;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using MediatR;

namespace API.RequestHandlers.CommandHandlers
{
    public class RemoveProductFromCartRequestHandler : IRequestHandler<RemoveProductFromCartRequestModel, InnerResult>
    {
        private IUserRetrieverService _userRetrieverService;
        private IUserDataService _userDataService;

        public RemoveProductFromCartRequestHandler(IUserRetrieverService userRetrieverService, IUserDataService userDataService)
        {
            _userRetrieverService = userRetrieverService;
            _userDataService = userDataService;
        }

        public async Task<InnerResult> Handle(RemoveProductFromCartRequestModel request, CancellationToken cancellationToken)
        {
            var resp1 = _userRetrieverService.GetUserId(request.User);
            if (resp1.IsFailed)
                return InnerResult.Fail(resp1.Errors, resp1.StatusCode);

            var id = resp1.Value;

            var resp2 = await _userDataService.RemoveFromCartAsync(id, request.ProductId, request.Quantity);
            if (resp2.IsFailed)
                return resp2;
            return InnerResult.Ok();
        }
    }
}
