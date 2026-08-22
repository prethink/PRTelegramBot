using PRTelegramBot.Attributes;

namespace PRTelegramBot.Exceptions
{
    /// <summary>
    /// Thrown when an inline command has no header registered for it.
    /// </summary>
    public class InlineCommandNotFoundException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="enum">The command enum value that was not found.</param>
        public InlineCommandNotFoundException(Enum @enum) 
            : base($"{@enum.GetType().Name}.{@enum} Inline command not found in collection. " +
                   $"Required add attribute [{nameof(InlineCommandAttribute)}] to the enum {@enum.GetType().Name}.") { }
    }
}
