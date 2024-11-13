using E_Commerce.API.Models.DTOs.Enums;

namespace E_Commerce.API.Models.DTOs
{
    public class ResponseModel<TResult>
    {
        public ResponseModel(bool isSuccess, ResponseCode code, string? message = null, TResult? result = default)
        {
            Code = code;
            IsSuccess = isSuccess;
            Result = result;
            Message = message;
        }

        public bool IsSuccess { get; }
        public TResult? Result { get; }
        public string? Message { get; }
        public ResponseCode Code { get; }

        public static ResponseModel<TResult> GetSuccess(TResult result, string? message = "All is ok!")
        {
            return new(true, ResponseCode.OK, message, result);
        }
        public static ResponseModel<TResult> GetFail(string? message = null)
        {
            return new(false, ResponseCode.NotSpecified, message);
        }
        public static ResponseModel<TResult> GetFail(ResponseCode responseCode, string? message = null)
        {
            return new(false, responseCode, message);
        }
    }
}
