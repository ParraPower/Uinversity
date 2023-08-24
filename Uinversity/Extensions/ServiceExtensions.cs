using LoggerService.Implementations;
using LoggerService.Interfaces;
using University.Implementations;
using University.Interfaces;
using University.Models;

namespace University.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureExceptionManager(this IServiceCollection services)
        {
            services.AddSingleton<IExceptionManager, GlobalExceptionManager>();
        }

        public static void ConfigureRedisCache(this IServiceCollection services, ConfigurationManager configuration)
        {
            var redisConfig = configuration.GetSection("Redis").Get<RedisConfig>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{redisConfig.ServerName}:{redisConfig.PortNumber}";
            });
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
        }
    }
}
