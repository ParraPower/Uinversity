using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityData.Implementations.Managers;
using UniversityData.Interfaces.Managers;

namespace UniversityData.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDataManagers(this IServiceCollection services)
        {
            services.AddScoped<IEnrollmentDataManager, EnrollmentDataManager>();
        }
    }
}
