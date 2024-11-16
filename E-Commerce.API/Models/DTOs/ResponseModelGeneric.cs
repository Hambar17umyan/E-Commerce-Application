using E_Commerce.API.Models.DTOs.Enums;
using System.Diagnostics.CodeAnalysis;

namespace E_Commerce.API.Models.DTOs
{
    public class ResponseModel<TResult>
    {
        private ResponseModel(TResult? result, string? message, ResponseCode code)
        {
            Result = result;
            Message = message;
            Code = code;
        }

        public bool IsSuccess => Result == null;
        public readonly TResult? Result;
        public readonly string? Message;
        public readonly ResponseCode Code;

        public static ResponseModel<TResult> GetSuccess(TResult result, string? message = "All is ok!")
        {
            if(result == null)
                throw new ArgumentNullException("A success should have a Result!");

            return new(result, message, ResponseCode.OK);
        }
        public static ResponseModel<TResult> GetFail(string? message = null)
        {
            return new(default, message, ResponseCode.NotSpecified);
        }
        public static ResponseModel<TResult> GetFail(ResponseCode responseCode, string? message = null)
        {
            return new(default, message, responseCode);
        }
    }
}
