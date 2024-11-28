using API.Models.Domain;
using API.Models.Request;
using API.Services.DataServices;
using FluentResults;
using MediatR;
using System.Transactions;

namespace API.RequestHandlers
{
    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequestModel, Result<IEnumerable<User>>>
    {
        private UserDataService _userDataService;

        public GetAllUsersRequestHandler(UserDataService userDataService)
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
