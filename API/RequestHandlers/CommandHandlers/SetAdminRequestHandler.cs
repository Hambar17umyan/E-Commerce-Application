using API.Models.Control.ResultModels;
using API.Models.Request.Commands;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;
using System.Net;

namespace API.RequestHandlers.CommandHandlers
{
    public class SetAdminRequestHandler : IRequestHandler<SetAdminRequestModel, InnerResult>
    {
        private IUserDataService _userDataService;
        private IRoleDataService _roleDataService;

        public SetAdminRequestHandler(IUserDataService userDataService, IRoleDataService roleDataService)
        {
            _userDataService = userDataService;
            _roleDataService = roleDataService;
        }

        public async Task<InnerResult> Handle(SetAdminRequestModel request, CancellationToken cancellationToken)
        {
            var resp = _userDataService.GetById(request.UserId);
            if (resp.IsSuccess)
            {
                var user = resp.Value;
                if (user.Roles.Any(x => x.Name == "Admin"))
                {
                    return InnerResult.Fail("The user is already an admin!", HttpStatusCode.BadRequest);
                }
                await _userDataService.UpdateAsync(user.Id, x => x.Roles.Add(_roleDataService.GetAdmin()));
                return InnerResult.Ok();
            }
            else
            {
                return InnerResult.Fail(resp.Errors, resp.StatusCode);
            }
        }
    }
}
