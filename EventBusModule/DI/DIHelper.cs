using EventBusModule.NotificationService;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusModule.DI
{
    public static class DIHelper
    {
        public static void EventBusModuleDIAdd(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddTransient<INotificationService, NotificationService.NotificationService>();
        }
    }
}
