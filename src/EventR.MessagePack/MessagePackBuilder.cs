namespace EventR.MessagePack
{
    using EventR.Abstractions;
    using global::MessagePack;
    using global::MessagePack.Resolvers;

    /// <summary>
    /// Fluent API configuration related to EventR.MessagePack module features.
    /// https://github.com/neuecc/MessagePack-CSharp
    /// </summary>
    public class MessagePackBuilder : BuilderBase
    {
        private MessagePackSerializerOptions serializerOptions;
        private bool useTypeNameAliasing;

        public MessagePackBuilder(ConfigurationContext context)
            : base(context)
        {
            context.RegisterSerializer(CreateSerializer, Serializer.Id);
        }

        public static MessagePackSerializerOptions CreateDefaultSerializerOptions()
        {
            return MessagePackSerializerOptions.Standard
                .WithResolver(CompositeResolver.Create(
                    NativeGuidResolver.Instance,
                    NativeDecimalResolver.Instance,
                    NativeDateTimeResolver.Instance,
                    StandardResolver.Instance))
                .WithOmitAssemblyVersion(true)
                .WithCompression(MessagePackCompression.Lz4BlockArray);
        }

        private ISerializeEvents CreateSerializer(IEventFactory eventFactory)
        {
            var translator = useTypeNameAliasing ? new TypeIdTranslator(eventFactory.GetKnownEvents()) : null;
            var options = serializerOptions ?? CreateDefaultSerializerOptions();
            return new Serializer(options, eventFactory, translator);
        }

        public MessagePackBuilder WithSerializerOptions(MessagePackSerializerOptions options)
        {
            Expect.NotNull(options, nameof(options));
            serializerOptions = options;
            return this;
        }

        public MessagePackBuilder UseTypeNameAliasing()
        {
            useTypeNameAliasing = true;
            return this;
        }
    }
}
