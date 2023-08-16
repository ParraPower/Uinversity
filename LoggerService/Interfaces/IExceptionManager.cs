using LoggerService.Models;
using Microsoft.AspNetCore.Http;

namespace LoggerService.Interfaces
{
    public interface IExceptionManager
    {
        public Task<ExceptionManagerHandleExceptionResponse> HandleExceptionAsync(HttpContext context, Exception exception);
    }
}
