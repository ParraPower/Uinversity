using Microsoft.AspNetCore.Http;
using System.Net;

namespace University.Models.Validation
{
    public interface IApiException
    {
        public HttpStatusCode GetHttpStatusCode();
    }

    public abstract class ApiException : Exception, IApiException
    {
        public readonly HttpStatusCode _statusCode = HttpStatusCode.InternalServerError;

        public ApiException(string message)  : base(message)
        {
            
        }

        public ApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            _statusCode = statusCode;
        }

        public HttpStatusCode GetHttpStatusCode()
        {
            return _statusCode;
        }
    }
}
