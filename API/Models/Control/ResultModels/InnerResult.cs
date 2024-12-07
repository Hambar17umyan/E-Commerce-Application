using FluentResults;
using System.Net;

namespace API.Models.Control.ResultModels
{
    public class InnerResult : Result
    {
        private InnerResult()
        {

        }

        private InnerResult(IEnumerable<IError> errors)
        {
            WithErrors(errors);
        }

        private InnerResult(ISuccess success)
        {
            WithSuccess(success);
        }

        public HttpStatusCode StatusCode { get; set; }

        public static InnerResult Ok(HttpStatusCode status = HttpStatusCode.OK)
        {
            return new InnerResult
            {
                StatusCode = status,
            };
        }

        public static InnerResult Ok(string success, HttpStatusCode status = HttpStatusCode.OK)
        {
            return  new InnerResult(new Success(success))
            {
                StatusCode = status,
            };
        }

        public static InnerResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            var innerResult = new InnerResult();
            innerResult.WithError(errorMessage);
            innerResult.StatusCode = status;
            return innerResult;
        }

        public static InnerResult Fail(IEnumerable<string> errorMessages, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            var innerResult = new InnerResult();
            foreach (var error in errorMessages)
            {
                innerResult.WithError(error);
            }
            innerResult.StatusCode = status;
            return innerResult;
        }
        public static InnerResult Fail(IEnumerable<IError> errors, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            var res = new InnerResult(errors);
            res.StatusCode = status;
            return res;
        }
        public static InnerResult Fail(IError error, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            var res = new InnerResult(new List<IError>() { error });
            res.StatusCode = status;
            return res;
        }
    }
}
