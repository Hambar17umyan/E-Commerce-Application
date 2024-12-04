using API.Models.Request;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers
{
    public class SetAdminRequestHandler : IRequestHandler<SetAdminRequestModel, Result>
    {
        private IUserDataService _userDataService;
        private IRoleDataService _roleDataService;

        public SetAdminRequestHandler(IUserDataService userDataService, IRoleDataService roleDataService)
        {
            _userDataService = userDataService;
            _roleDataService = roleDataService;
        }

        public Task<Result> Handle(SetAdminRequestModel request, CancellationToken cancellationToken)
        {
            var resp = _userDataService.GetById(request.UserId);
            if (resp.IsSuccess)
            {
                var user = resp.Value;
                if (user.Roles.Any(x => x.Name == "Admin"))
                {
                    return Task.FromResult(Result.Fail("The user is already an admin!"));
                }
                _userDataService.UpdateAsync(user.Id, x => x.Roles.Add(_roleDataService.GetAdmin()));
                return Task.FromResult(Result.Ok());
            }
            else
            {
                return Task.FromResult(Result.Fail(resp.Errors));
            }
        }
    }
}
