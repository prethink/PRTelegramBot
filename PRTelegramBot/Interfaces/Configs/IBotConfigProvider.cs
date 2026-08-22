namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the bot configuration provider.
    /// </summary>
    public interface IBotConfigProvider
    {
        /// <summary>
        /// Sets the path to the configuration file.
        /// </summary>
        /// <param name="configPath">Path to the file.</param>
        public void SetConfigPath(string configPath);

        /// <summary>
        /// Gets the parameters from the configuration file.
        /// </summary>
        /// <typeparam name="TOptions">The options class.</typeparam>
        /// <returns>Parameters.</returns>
        public TOptions GetOptions<TOptions>() where TOptions : class;

        /// <summary>
        /// Gets the value of the parameter.
        /// </summary>
        /// <typeparam name="TReturn">Type of the returned value.</typeparam>
        /// <param name="optionName">Parameter name.</param>
        /// <returns>The parameter value.</returns>
        public TReturn GetValue<TReturn>(string optionName);

        /// <summary>
        /// Gets a key-value dictionary from the configuration file.
        /// </summary>
        /// <returns>A key-value dictionary.</returns>
        public Dictionary<string, string> GetKeysAndValues();

        /// <summary>
        /// Gets the key-value pairs from the configuration file's parameters.
        /// </summary>
        /// <typeparam name="TOptions">The options class.</typeparam>
        /// <returns>A key-value dictionary.</returns>
        public Dictionary<string, string> GetKeysAndValuesByOptions<TOptions>() where TOptions : class;
    }
}
