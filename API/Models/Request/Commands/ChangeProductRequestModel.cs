using API.Models.Control.ResultModels;
using FluentResults;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace API.Models.Request.Commands
{
    public class ChangeProductRequestModel : IRequest<InnerResult>
    {
        public int ProductId { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public decimal? NewPrice { get; set; }
    }
}
