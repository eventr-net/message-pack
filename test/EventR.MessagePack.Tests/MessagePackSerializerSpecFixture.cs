namespace EventR.MessagePack.Tests
{
    using EventR.Abstractions;
    using EventR.Spec;
    using EventR.Spec.Serialization;
    using System.Linq;

    public sealed class MessagePackSerializerSpecFixture : ISerializerSpecFixture
    {
        public object[] EventsFromDomain { get; }

        public ISerializeEvents Serializer { get; }

        public string Description => "Default settings: with type ID shortening and native resolvers for DateTime, Guid and decimal.";

        public MessagePackSerializerSpecFixture()
        {
            var services = Helper.CreateAggregateRootServices(0);
            var rootAggregate = UseCases.Full().AsDirtyCustomerAggregate(services);
            EventsFromDomain = rootAggregate.UncommitedEvents;

            var eventTypes = EventsFromDomain.Select(x => x.GetType()).ToArray();
            var options = MessagePackBuilder.CreateDefaultSerializerOptions();
            Serializer = new Serializer(options, new EventFactory(eventTypes), new TypeIdTranslator(eventTypes));
        }
    }
}
