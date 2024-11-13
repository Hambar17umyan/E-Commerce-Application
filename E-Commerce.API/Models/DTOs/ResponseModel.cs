using E_Commerce.API.Models.DTOs.Enums;
using System.Diagnostics.CodeAnalysis;

namespace E_Commerce.API.Models.DTOs
{
    public class ResponseModel
    {
        public ResponseModel(bool isSuccess, ResponseCode code, string? message = null)
        {
            Code = code;
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }
        public string? Message { get; }
        public ResponseCode Code { get; }

        public static ResponseModel GetSuccess(string? message = "All is ok!")
        {
            return new(true, ResponseCode.OK, message);
        }
        public static ResponseModel GetFail(string? message = null)
        {
            return new(false, ResponseCode.NotSpecified, message);
        }
        public static ResponseModel GetFail(ResponseCode responseCode, string? message = null)
        {
            return new(false, responseCode, message);
        }
    }
}
