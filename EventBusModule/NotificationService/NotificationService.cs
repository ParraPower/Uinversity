using Azure;
using Azure.Messaging.EventGrid;
using SDK.EventBus;
using System.Text.Json;

namespace EventBusModule.NotificationService
{
    public interface INotificationService
    {
        public Task SendMessage(EventMessage eventMessage);
    }

    public class NotificationService : INotificationService
    {
        private readonly string topicEndpoint = "https://universityeventsbroadcaster.australiaeast-1.eventgrid.azure.net/api/events";
        private readonly string topicKey = "5ubaycA3MrJqNtoIZu7Bjv039nH9t07aNrxohsp5w7cZJfwIrJqWJQQJ99BAACL93NaXJ3w3AAABAZEGGu45";

        public async Task SendMessage(EventMessage eventMessage)
        {
            var topicCredentials = new AzureKeyCredential(topicKey);
            var client = new EventGridPublisherClient(new Uri(topicEndpoint), topicCredentials);

            await client.SendEventAsync(new EventGridEvent(
                "Event Message",
                eventMessage.Type.ToString(),
                eventMessage.Version,
                JsonSerializer.Serialize(eventMessage)
            ));

            Console.WriteLine("Event published successfully.");
        }
    }
}