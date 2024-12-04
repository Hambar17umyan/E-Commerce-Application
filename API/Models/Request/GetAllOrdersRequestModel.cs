using API.Models.Domain;
using FluentResults;
using MediatR;

namespace API.Models.Request
{
    public class GetAllOrdersRequestModel : IRequest<Result<IEnumerable<Order>>>
    {
    }
}
