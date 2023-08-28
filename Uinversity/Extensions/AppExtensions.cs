using LoggerService.Interfaces;
using LoggerService.Middleware;
using Microsoft.EntityFrameworkCore;
using UniversityData.Context;

namespace University.Extensions
{
    public static class AppExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void LogAppStarted(this WebApplication app)
        {
            var logger = app.Services.GetService<ILoggerManager>() ?? throw new Exception("Logger is null");
            logger.LogDebug("App has started");
        }

        public async static void ApplyDatabaseMigrations(this WebApplication app)
        {
            var logger = app.Services.GetService<ILoggerManager>() ?? throw new Exception("Logger is null");

            logger.LogDebug("Database migration is starting");

            using var scope = app.Services.CreateAsyncScope();
            using var db = scope.ServiceProvider.GetService<UniversityContext>() ?? throw new Exception("Database context is null");
            
            await db.Database.MigrateAsync();

            logger.LogDebug("Database migration is finished");
        }
    }
}
