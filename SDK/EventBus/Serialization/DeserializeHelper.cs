using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SDK.EventBus.Events;

namespace SDK.EventBus.Serialization
{
    public static class DeserializeHelper
    {
        public static EventMessage Convert(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException();

            var jObj = JsonConvert.DeserializeObject(body);

            var typeStr = (jObj as JObject).Property("Type").Value.ToString();

            int.TryParse(typeStr, out var type);

            switch ((EvenetMessageType)type)
            {
                case EvenetMessageType.StudentActivity:
                    return JsonConvert.DeserializeObject<StudentActitvityEventMessage>(body) ?? throw new ArgumentException("not in correct format");
            }

            throw new ArgumentException("No event type found matching that which was provided");
        }
    }
}
