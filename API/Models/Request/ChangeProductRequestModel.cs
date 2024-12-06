using FluentResults;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace API.Models.Request
{
    public class ChangeProductRequestModel : IRequest<Result>
    {
        public int ProductId { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public decimal? NewPrice { get; set; }
    }
}
