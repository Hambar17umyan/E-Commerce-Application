using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using FluentResults;
using MediatR;

namespace API.Models.Request.Queries
{
    public class GetAllProductsRequestModel : IRequest<InnerResult<IEnumerable<ProductOutputModel>>>
    {
    }
}
