﻿using LoggerService.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using University.Implementations;
using University.Interfaces;
using UniversityCore.Models.Config;
using UniversityData.Context;

namespace University.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, ConfigurationManager configuration) 
        {
            var sqlOptionsSection = configuration.GetSection("SqlOptions");

            services.Configure<SqlOptions>(sqlOptionsSection);

            var sqlOptions = sqlOptionsSection.Get<SqlOptions>();
            services.AddDbContext<UniversityContext>(options =>
                options.UseSqlServer(sqlOptions.ConnectionString));
        }

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
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            // Register MediatR services
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }
    }
}
