using FluentResults;
using System.Net;

namespace API.Models.Control.ResultModels
{
    public class InnerResult : Result
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
