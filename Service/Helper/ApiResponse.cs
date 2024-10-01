using Azure.Core;

namespace Service.Helper;

public class ApiResponse
{
    public ApiResponse(object? data, string message, int statusCode)
    {
        Data = data;
        Message = message;
        StatusCode = statusCode;
    }
    public object? Data { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}