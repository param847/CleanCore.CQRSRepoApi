namespace Shared.Models.ResponseModel
{
    /// <summary>
    /// Universal API response envelope.
    /// </summary>
    public class ResponseData<T>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsSuccess { get; set; } = false;
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public string Exception { get; set; } = string.Empty;
        public string ExceptionDetails { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public T Data { get; set; } = default!;
    }
}