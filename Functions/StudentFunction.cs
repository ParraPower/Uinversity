// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using SDK.EventBus.Events;

namespace Functions
{


    public class StudentFunction
    {
        private const string EventQueueConnectionString = "DefaultEndpointsProtocol=https;AccountName=universitystoragealmo;AccountKey=2prtbW/N9oesvpKyER7nmcpCeXREGzD03Ma6EXSoX+0kZL7p/99EyYyduNLedhAk5jXXE3HgaycH+AStyrtLWQ==;EndpointSuffix=core.windows.net";
        //private const string SignalRConnectionString = "Endpoint=https://universityeventbroadcaster.service.signalr.net;AccessKey=5kMc3d7KVP0ZVM1FGmA0LHYzBp3qn2tzlZz2csZaKpVrCXNCZ2qCJQQJ99BAACL93NaXJ3w3AAAAASRSdCW0;Version=1.0;";

        private readonly ILogger<StudentFunction> _logger;

        public StudentFunction(ILogger<StudentFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(StudentFunction))]
        //[QueueOutput("eventqueue", Connection = EventQueueConnectionString)]
        public async Task<string> Run([EventGridTrigger] CloudEvent cloudEvent
            )
        {
            _logger.LogInformation("Event type: {type}, Event subject: {subject}", cloudEvent.Type, cloudEvent.Subject);

            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (cloudEvent.Data != null)
            {
                var t = cloudEvent.Data.ToString();
                _logger.LogInformation("Event t: {t}", t);
            }

            try
            {
                string requestBody = await new StreamReader(cloudEvent.Data.ToStream()).ReadToEndAsync();
                var data = SDK.EventBus.Serialization.DeserializeHelper.Convert(requestBody);

                _logger.LogInformation($"Message '{requestBody}' has been added to the queue.");

                return requestBody;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);
                throw new InvalidProgramException();
            }
        }

        //[FunctionName("negotiate")]
        //public static Microsoft.Azure.Functions.Worker.SignalRConnectionInfo GetSignalRInfo(
        //[HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
        //[SignalRConnectionInfo(HubName = "broadcast", ConnectionStringSetting = SignalRConnectionString)] Microsoft.Azure.Functions.Worker.SignalRConnectionInfo connectionInfo,
        //ILogger log)
        //{
        //    log.LogInformation("Negotiation requested.");
        //    return connectionInfo;
        //}

        //[Function(nameof(SendToUser))]
        //[SignalROutput(HubName = "broadcast", ConnectionStringSetting = SignalRConnectionString)]
        //public static SignalRMessageAction SendToUser([QueueTrigger("eventqueue", Connection = EventQueueConnectionString)] QueueMessage myQueueItem)
        //{
        //    var bodyReader = new StreamReader(myQueueItem.Body.ToStream());
        //    return new SignalRMessageAction("newMessage")
        //    {
        //        Arguments = new[] { bodyReader.ReadToEnd() },
        //        UserId = "userToSend",
        //    };
        //}
    }
}
