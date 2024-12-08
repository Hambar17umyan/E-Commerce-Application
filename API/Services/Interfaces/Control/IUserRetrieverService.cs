using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using System.Security.Claims;

namespace API.Services.Interfaces.Control
{
    public interface IUserRetrieverService
    {
        InnerResult<User> GetUser(ClaimsPrincipal principal);
        InnerResult<int> GetUserId(ClaimsPrincipal principal);
    }
}
