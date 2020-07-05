namespace EventR.MessagePack.Tests
{
    using EventR.Spec.Serialization;

    public class MessagePackSerializerSpec : SerializerSpec<MessagePackSerializerSpecFixture>
    {
        public MessagePackSerializerSpec(MessagePackSerializerSpecFixture fixture)
            : base(fixture)
        {
        }
    }
}
