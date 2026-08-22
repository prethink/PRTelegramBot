using System.Text.RegularExpressions;

namespace PRTelegramBot.Builders
{
    /// <summary>
    /// Message builder with support for named tokens and positional arguments.
    /// Lets you compose strings in the style of <see cref="string.Format"/>, 
    /// but extended with tokens such as {QA}, {Dev} and so on.
    /// </summary>
    public class MessageBuilder
    {
        #region Fields and properties

        /// <summary>
        /// The message template.
        /// </summary>
        private string template;

        /// <summary>
        /// Dictionary of resolvers for the named tokens.
        /// The key is the token name, the value is a function that returns a string.
        /// </summary>
        private readonly Dictionary<string, Func<string>> resolvers = new();

        /// <summary>
        /// The list of positional arguments to substitute into {0}, {1} and so on.
        /// </summary>
        private readonly List<object> args = new();

        #endregion

        #region Methods

        /// <summary>
        /// Adds a named token with a lazy resolver (Func&lt;string&gt;).
        /// </summary>
        /// <param name="key">Name of the token in the template, for example "QA".</param>
        /// <param name="resolver">A function that returns the token value when Build() is called.</param>
        /// <returns>The current <see cref="MessageBuilder"/> instance for the fluent API.</returns>
        public MessageBuilder AddResolver(string key, Func<string> resolver)
        {
            resolvers[key] = resolver;
            return this;
        }

        /// <summary>
        /// Adds a named token with a static value.
        /// </summary>
        /// <param name="key">Name of the token in the template.</param>
        /// <param name="value">The token's string value.</param>
        /// <returns>The current <see cref="MessageBuilder"/> instance for the fluent API.</returns>
        public MessageBuilder AddResolver(string key, string value)
        {
            resolvers[key] = () => value;
            return this;
        }

        /// <summary>
        /// Adds a single positional argument to substitute into {0}, {1} and so on.
        /// </summary>
        /// <param name="arg">Argument to substitute.</param>
        /// <returns>The current <see cref="MessageBuilder"/> instance for the fluent API.</returns>
        public MessageBuilder AddArgument(object arg)
        {
            args.Add(arg);
            return this;
        }

        /// <summary>
        /// Adds several positional arguments at once.
        /// </summary>
        /// <param name="arguments">Array of arguments to substitute.</param>
        /// <returns>The current <see cref="MessageBuilder"/> instance for the fluent API.</returns>
        public MessageBuilder AddArguments(params object[] arguments)
        {
            args.AddRange(arguments);
            return this;
        }

        /// <summary>
        /// Builds the final string, substituting the positional arguments and the values of the named tokens.
        /// Tokens that are not found are left as {TokenName}.
        /// </summary>
        /// <returns>The resulting string with the values substituted in.</returns>
        public string Build()
        {
            return Regex.Replace(template, @"\{(.*?)\}", match =>
            {
                var key = match.Groups[1].Value;

                // Check for a positional argument
                if (int.TryParse(key, out var index))
                {
                    if (index < args.Count)
                        return args[index]?.ToString();

                    // If the index is missing, return the original token
                    return match.Value;
                }

                // Check for a named token
                if (resolvers.TryGetValue(key, out var resolver))
                    return resolver()?.ToString();

                // If the token is not found, return it as is
                return match.Value;
            });
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new message builder with the given template.
        /// </summary>
        /// <param name="template">A template string with tokens and positional arguments.</param>
        public MessageBuilder(string template)
        {
            this.template = template;
        }

        #endregion
    }
}
