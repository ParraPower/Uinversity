using LoggerService.Interfaces;
using LoggerService.Models;
using System.Net;

namespace University.Implementations
{
    public class GlobalExceptionManager : IExceptionManager
    {
        public async Task<ExceptionManagerHandleExceptionResponse> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());

            return new ExceptionManagerHandleExceptionResponse(true, true);
        }
    }
}
