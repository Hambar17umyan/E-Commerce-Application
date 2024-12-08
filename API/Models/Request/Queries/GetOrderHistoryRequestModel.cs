using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using MediatR;
using System.Security.Claims;

namespace API.Models.Request.Queries
{
    public class GetOrderHistoryRequestModel: IRequest<InnerResult<IEnumerable<OrderOutputModel>>>
    {
        internal ClaimsPrincipal User { get; set; }
    }
}
