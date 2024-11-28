using API.Models.Domain;
using FluentResults;
using MediatR;

namespace API.Models.Request
{
    public class GetAllUsersRequestModel : IRequest<Result<IEnumerable<User>>>
    {
    }
}
