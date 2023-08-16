using LoggerService.Interfaces;
using LoggerService.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        private readonly IExceptionManager _exceptionManager;
        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger, IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var extensionHandlerResponse = await _exceptionManager.HandleExceptionAsync(context, exception);

            if (extensionHandlerResponse.Implemented && extensionHandlerResponse.Successful)
            {
                return;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }
    }
}
