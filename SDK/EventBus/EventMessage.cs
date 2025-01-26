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

        public EventMessage(string version, string source, Guid correlationId, EvenetMessageType type, Guid? id, DateTimeOffset? generatedAt)
        {
            Version = version;
            Source = source;
            CorrelationId = correlationId;
            Type = type;
            Id = id ?? _id;
            GeneratedAt = generatedAt ?? _generatedAt;
        }

        public string Version { get; init; }
        public string Source { get; init; }
        public Guid CorrelationId { get; init; }
        public DateTimeOffset GeneratedAt { get; init; }
        public Guid Id { get; init; }
        public EvenetMessageType Type { get; init; }
    }
}
