namespace EventR.MessagePack
{
    using EventR.Abstractions;

    public static class Extensions
    {
        public static MessagePackBuilder MessagePack(this BuilderBase coreBuilder)
        {
            return new MessagePackBuilder(coreBuilder.Context);
        }
    }
}
