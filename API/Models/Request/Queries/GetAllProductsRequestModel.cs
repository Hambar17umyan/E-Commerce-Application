using API.Models.Domain.Concrete;
using FluentResults;
using MediatR;

namespace API.Models.Request.Queries
{
    public class GetAllProductsRequestModel : IRequest<Result<IEnumerable<Product>>>
    {
    }
}
