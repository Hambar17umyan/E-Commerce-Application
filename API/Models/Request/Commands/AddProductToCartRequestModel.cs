using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using MediatR;
using System.Security.Claims;

namespace API.Models.Request.Commands
{
    public class AddProductToCartRequestModel : IRequest<InnerResult>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        internal ClaimsPrincipal User { get; set; }
    }
}
