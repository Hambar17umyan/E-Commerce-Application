using API.Models.Domain;
using FluentResults;
using MediatR;

namespace API.Models.Request
{
    public class GetAllProductsRequestModel : IRequest<Result<IEnumerable<Product>>>
    {
    }
}
