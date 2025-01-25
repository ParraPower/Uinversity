using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.EventBus
{
    public enum EvenetMessageType
    {
        None = 0,
        StudentActivity = 1
    }

    public abstract class EventMessage
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly DateTimeOffset _generatedAt = DateTimeOffset.UtcNow;

        public EventMessage(string version, string source, Guid correlationId, EvenetMessageType type)
        {
            Version = version;
            Source = source;
            CorrelationId = correlationId;
            Type = type;
        }

        public string Version { get; init; }
        public string Source { get; init; }
        public Guid CorrelationId { get; init; }
        public DateTimeOffset GeneratedAt { get { return _generatedAt; } }
        public Guid Id { get { return _id; } }
        public EvenetMessageType Type { get; init; }
    }
}
