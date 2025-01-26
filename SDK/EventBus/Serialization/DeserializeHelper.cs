using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SDK.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SDK.EventBus.Serialization
{
    public static class DeserializeHelper
    {
        public static EventMessage Convert(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException();

            var obj = JsonConvert.DeserializeObject<JObject>(body);
            int.TryParse(obj?.Property("Type")?.Value.ToString(), out int type); //?? throw new ArgumentNullException("Type field not found") ?? new ArgumentNullException("object passed in not in correct format");
            
            switch ((EvenetMessageType)type)
            {
                case EvenetMessageType.StudentActivity:
                    return JsonConvert.DeserializeObject<StudentActitvityEventMessage>(body) ?? throw new ArgumentException("not in correct format");
            }

            throw new ArgumentException("No event type found matching that which was provided");
        }
    }
}
