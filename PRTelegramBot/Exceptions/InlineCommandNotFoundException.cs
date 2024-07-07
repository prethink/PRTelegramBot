using PRTelegramBot.Attributes;

namespace PRTelegramBot.Exceptions
{
    public class InlineCommandNotFoundException : Exception
    {
        public InlineCommandNotFoundException(Enum @enum) 
            : base($"{@enum.GetType().Name}.{@enum} Inline command not found in collection. " +
                   $"Required add attribute [{nameof(InlineCommandAttribute)}] to the enum {@enum.GetType().Name}.") { }
    }
}
