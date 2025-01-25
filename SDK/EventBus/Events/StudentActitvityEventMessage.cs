using System;

namespace SDK.EventBus.Events
{
    public enum StudentActitvityEventType
    {
        None = 0,
        Added = 1,
        Updated = 2,
        Deleted = 3,
        Enrolled = 4,
        Unlisted = 5,
    }

    public class StudentActitvityEventMessage : EventMessage
    {
        public StudentActitvityEventMessage(
            string version, string source, Guid correlationId,
            StudentActitvityEventType studentActitvityEventType, long studentId, long actionId) :
            base(version, source, correlationId, EvenetMessageType.StudentActivity)
        {
            EventType = studentActitvityEventType;
            StudentId = studentId;
            ActionId = actionId;
        }

        public StudentActitvityEventType EventType { get; init;  }
        public long StudentId { get; init; }
        public long ActionId { get; init; }
    }

    public static class StudentActitvityEventMessageFactory
    {
        public static StudentActitvityEventMessage Generate(
            string version, string source, Guid correlationId,
            StudentActitvityEventType studentActitvityEventType, long studentId, long actionId)
        {
            return new StudentActitvityEventMessage(version, source, correlationId, studentActitvityEventType, studentId, actionId);
        }
    }
}