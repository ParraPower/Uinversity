using LoggerService.Interfaces;
using LoggerService.Models;
using System.Net;
using University.Models.Validation;

namespace University.Implementations
{
    public class GlobalExceptionManager : IExceptionManager
    {
        private readonly HttpStatusCode _defaultStatusCode = HttpStatusCode.InternalServerError;
        private readonly string _defaultExceptionMessage = "Internal Server Error from the custom middleware.";
        public async Task<ExceptionManagerHandleExceptionResponse> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = (int)(exception.GetType() == typeof(ApiException) ? ((ApiException)exception).GetHttpStatusCode() : _defaultStatusCode),
                Message = exception.GetType() == typeof(ApiException) && !string.IsNullOrWhiteSpace(exception.Message) 
                    ? exception.Message : _defaultExceptionMessage
            }.ToString());

            return new ExceptionManagerHandleExceptionResponse(true, true);
        }
    }
}
