using System;
namespace Service.ExceptionHandlings
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public ApiException(int statusCode, string message, string detail = "") : base(message)
        {
            StatusCode = statusCode;
            Message = message;
            Detail = detail;
        }
    }
}

