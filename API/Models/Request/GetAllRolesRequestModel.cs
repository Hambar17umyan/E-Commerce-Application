using API.Models.Domain;
using FluentResults;
using MediatR;

namespace API.Models.Request
{
    public class GetAllRolesRequestModel : IRequest<Result<IEnumerable<Role>>>
    {
    }
}
