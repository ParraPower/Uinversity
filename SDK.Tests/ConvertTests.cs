using SDK.EventBus.Events;
using SDK.EventBus.Serialization;

namespace SDK.Tests
{
    public class ConvertTests
    {
        [Fact]
        public void Test1()
        {
            var a = "{\u0022Version\u0022:\u00221.1\u0022,\u0022Source\u0022:\u0022CreateEnrollment\u0022,\u0022CorrelationId\u0022:\u0022d4674bf2-2b30-48a7-a9ce-859e3e9c2f9a\u0022,\u0022GeneratedAt\u0022:\u00222025-01-26T10:42:53.1739359\u002B00:00\u0022,\u0022Id\u0022:\u0022364ff32f-f51a-48d4-804b-386444ef92ad\u0022,\u0022Type\u0022:1}";
            var eventMessage = DeserializeHelper.Convert(a);
            var type = eventMessage.Type;
            Assert.Equal(EventBus.EvenetMessageType.StudentActivity, type);
            Assert.True(true);
        }
    }
}