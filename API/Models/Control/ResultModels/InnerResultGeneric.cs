using FluentResults;
using System.Net;

namespace API.Models.Control.ResultModels
{
    public class InnerResult<TResult> : Result<TResult>
    {
        private InnerResult(TResult value, HttpStatusCode status = HttpStatusCode.OK)
        {
            WithValue(value);
        }
        private InnerResult()
        {
            
        }
        private InnerResult(IEnumerable<IError> errors)
        {
            WithErrors(errors);
        }
        public HttpStatusCode StatusCode { get; set; }

        public static InnerResult<TResult> Ok(TResult result, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new InnerResult<TResult>(result)
            {
                StatusCode = status
            };
        }

        public static InnerResult<TResult> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            var innerResult = new InnerResult<TResult>();
            innerResult.WithError(errorMessage);
            innerResult.StatusCode = status;
            return innerResult;
        }

        public static InnerResult<TResult> Fail(IEnumerable<string> errorMessages, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            var innerResult = new InnerResult<TResult>();
            foreach (var error in errorMessages)
            {
                innerResult.WithError(error);
            }
            innerResult.StatusCode = status;
            return innerResult;
        }
        public static InnerResult<TResult> Fail(IEnumerable<IError> errors, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            var res = new InnerResult<TResult>(errors);
            res.StatusCode = status;
            return res;
        }

        public static implicit operator InnerResult<TResult>(TResult value)
        {
            return Ok(value);
        }

        public static InnerResult<TResult> Fail(IError error, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            var res = new InnerResult<TResult>(new List<IError>() { error });
            res.StatusCode = status;
            return res;
        }

        public static implicit operator TResult(InnerResult<TResult> result)
        {
            return result.Value;
        }
    }
}
