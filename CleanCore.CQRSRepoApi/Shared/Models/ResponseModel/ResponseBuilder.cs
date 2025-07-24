namespace Shared.Models.ResponseModel
{
    public static class ResponseBuilder
    {
        public static ResponseData<T> Success<T>(T data, string message = "", string token = "", int statusCode = 200) =>
            new ResponseData<T>
            {
                IsSuccess = true,
                StatusCode = statusCode,
                Message = message,
                Data = data!,
                Token = token
            };

        public static ResponseData<T> Failure<T>(int statusCode, string message, Exception? ex = null) =>
            new ResponseData<T>
            {
                IsSuccess = false,
                StatusCode = statusCode,
                Message = message,
                Exception = ex?.GetType().Name ?? "",
                ExceptionDetails = ex?.StackTrace ?? ""
            };
    }
}