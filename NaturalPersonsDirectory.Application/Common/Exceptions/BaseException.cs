using Microsoft.Extensions.Localization;
using System.Net;

namespace NaturalPersonsDirectory.Application.Common.Exceptions;

public class BaseException : Exception
{
    public BaseException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
}
