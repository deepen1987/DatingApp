namespace WebAPI.Errors;

public class ApiExceptions(int statusCode, string message, string? details = null)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Detail { get; set; } = details;
}