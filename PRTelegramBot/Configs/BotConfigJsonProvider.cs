using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Провайдер работы с конфигурационными json файлами.
    /// </summary>
    public class BotConfigJsonProvider : IBotConfigProvider
    {
        #region Поля и свойства

        private IConfigurationRoot configuration { get; set; }

        /// <summary>
        /// Путь до json файла.
        /// </summary>
        private string configPath { get; set; }

        #endregion

        #region IBotConfigProvider

        public void SetConfigPath(string configPath)
        {
            this.configPath = configPath;
            configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath).Build();
        }

        public TOptions GetOptions<TOptions>() 
            where TOptions : class
        {
            var section = configuration.GetSection(typeof(TOptions).Name);
            return section.Get<TOptions>();
        }

        public TReturn GetValue<TReturn>(string section)
        {
            return configuration.GetSection(section).Get<TReturn>();
        }

        public Dictionary<string, string> GetKeysAndValues()
        {
            string json = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public Dictionary<string, string> GetKeysAndValuesByOptions<T>() 
            where T : class
        {
            return configuration.GetSection(typeof(T).Name).AsEnumerable()
                .Where(x => !string.IsNullOrEmpty(x.Value))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public BotConfigJsonProvider() { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configPath">Путь до json файла.</param>
        public BotConfigJsonProvider(string configPath)
        {
            SetConfigPath(configPath);
        }

        #endregion
    }
}
