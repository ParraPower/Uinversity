using LoggerService.Middleware;

namespace University.Extensions
{
    public static class AppExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

    }
}
