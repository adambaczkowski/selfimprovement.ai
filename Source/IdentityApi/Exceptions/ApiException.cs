using System.Net;

namespace IdentityApi.Exceptions;

public class ApiException : Exception
{
    public ApiException(string errorCode) :
        this("An error occurred while processing this request.", errorCode)
    {
    }

    public ApiException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public ApiException(string message, Exception innerException, string errorCode) : base(message,
        innerException)
    {
        ErrorCode = errorCode;
    }

    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.BadRequest;
    
    public string ErrorCode { get; }
}