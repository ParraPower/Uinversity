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
            base(version, source, correlationId, EvenetMessageType.StudentActivity, null, null)
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

                    var a = "{\"Version\":\"1.1\",\"Source\":\"CreateEnrollment\",\"CorrelationId\":\"db7a19c2-cee8-479d-b87f-c84154cf4b29\",\"GeneratedAt\":\"2025-01-26T00:24:06.6420193+00:00\",\"Id\":\"3ad82ee0-a360-441a-b40f-16a1fa963376\",\"Type\":1}";

            return new StudentActitvityEventMessage(version, source, correlationId, studentActitvityEventType, studentId, actionId);
        }
    }
}