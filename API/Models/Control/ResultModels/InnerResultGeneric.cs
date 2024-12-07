using FluentResults;
using System.Net;

namespace API.Models.Control.ResultModels
{
    public class InnerResult<TResult> : Result<TResult>
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
