using API.Models.Domain;
using FluentResults;
using MediatR;

namespace API.Models.Request
{
    public class GetAllInventoriesRequestModel : IRequest<Result<IEnumerable<Inventory>>>
    {
    }
}
