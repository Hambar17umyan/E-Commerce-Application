using API.Models.Domain.Concrete;
using API.Models.Request.Queries;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers.QueryHandlers
{
    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequestModel, Result<IEnumerable<User>>>
    {
        private IUserDataService _userDataService;

        public GetAllUsersRequestHandler(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public Task<Result<IEnumerable<User>>> Handle(GetAllUsersRequestModel request, CancellationToken cancellationToken)
        {
            var res = _userDataService.GetAll();
            return Task.FromResult(res);
        }
    }
}
