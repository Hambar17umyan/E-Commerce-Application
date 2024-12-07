using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using FluentResults;
using MediatR;

namespace API.Models.Request.Queries
{
    public class GetAllRolesRequestModel : IRequest<Result<IEnumerable<RoleOutputModel>>>
    {
    }
}
