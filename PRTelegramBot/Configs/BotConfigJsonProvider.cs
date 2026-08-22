using Microsoft.Extensions.Configuration;
using PRTelegramBot.Interfaces;
using System.Text.Json;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Provider that works with json configuration files.
    /// </summary>
    public sealed class BotConfigJsonProvider : IBotConfigProvider
    {
        #region Fields and properties

        private IConfigurationRoot configuration { get; set; }

        /// <summary>
        /// Path to the json file.
        /// </summary>
        private string configPath { get; set; }

        #endregion

        #region IBotConfigProvider

        /// <inheritdoc />
        public void SetConfigPath(string configPath)
        {
            this.configPath = configPath;
            configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath).Build();
        }

        /// <inheritdoc />
        public TOptions GetOptions<TOptions>() 
            where TOptions : class
        {
            var section = configuration.GetSection(typeof(TOptions).Name);
            return section.Get<TOptions>();
        }

        /// <inheritdoc />
        public TReturn GetValue<TReturn>(string section)
        {
            return configuration.GetSection(section).Get<TReturn>();
        }

        /// <inheritdoc />
        public Dictionary<string, string> GetKeysAndValues()
        {
            string json = File.ReadAllText(configPath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }

        /// <inheritdoc />
        public Dictionary<string, string> GetKeysAndValuesByOptions<T>() 
            where T : class
        {
            return configuration.GetSection(typeof(T).Name).AsEnumerable()
                .Where(x => !string.IsNullOrEmpty(x.Value))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public BotConfigJsonProvider() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configPath">Path to the json file.</param>
        public BotConfigJsonProvider(string configPath)
        {
            SetConfigPath(configPath);
        }

        #endregion
    }
}
