using API.Models.Control.ResultModels;
using FluentResults;
using MediatR;

namespace API.Models.Request.Commands
{
    public class AddProductRequestModel : IRequest<InnerResult>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
