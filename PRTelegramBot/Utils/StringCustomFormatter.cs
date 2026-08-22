using PRTelegramBot.Builders;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Utility for custom string formatting.
    /// Lets you build strings with positional arguments and named tokens.
    /// </summary>
    public class StringCustomFormatter
    {
        #region Methods

        /// <summary>
        /// Creates a new message builder with the given template.
        /// </summary>
        /// <param name="template">A string template with tokens, for example "{QA} tested {PR}, {0}"</param>
        /// <returns>The <see cref="MessageBuilder"/> instance, for adding further arguments and resolvers.</returns>
        public MessageBuilder Message(string template) => new MessageBuilder(template);

        #endregion
    }
}
