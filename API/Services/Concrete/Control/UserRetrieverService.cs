using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using Azure.Core;
using System.Security.Claims;

namespace API.Services.Concrete.Control
{
    public class UserRetrieverService : IUserRetrieverService
    {
        private IUserDataService _userDataService;

        public UserRetrieverService(IUserDataService userDataService)
        {
            this._userDataService = userDataService;
        }

        public InnerResult<User> GetUser(ClaimsPrincipal principal)
        {
            var userId = GetUserId(principal);
            if (userId.IsFailed)
                return InnerResult<User>.Fail(userId.Errors, userId.StatusCode);
            return _userDataService.GetById(userId);
        }

        public InnerResult<int> GetUserId(ClaimsPrincipal principal)
        {
            return int.Parse(principal.Claims.First(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti).Value);
        }
    }
}
