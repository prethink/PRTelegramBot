using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Configs
{
    public class BotConfigJsonProvider : IBotConfigProvider
    {
        #region Поля и свойства

        private IConfigurationRoot config { get; set; }

        private string configPath { get; set; }

        #endregion

        #region IBotConfigProvider

        public void SetConfigPath(string configPath)
        {
            this.configPath = configPath;
            config = new ConfigurationBuilder()
                .AddJsonFile(configPath).Build();
        }

        public T GetSettings<T>()
        {
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }

        public string GetValueByKey<T>(string key) where T : class
        {
            var sectionData = GetSettings<T>();
            var propertyInfo = typeof(T).GetProperty(key);
            var value = propertyInfo.GetValue(sectionData);
            return (string)value;
        }

        public TReturn GetValue<TReturn>(string section)
        {
            return config.GetSection(section).Get<TReturn>();
        }

        public Dictionary<string, string> GetKeysAndValues()
        {
            string json = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public Dictionary<string, string> GetKeysAndValuesBySection<T>()
        {
            return config.GetSection(typeof(T).Name).AsEnumerable()
                .Where(x => !string.IsNullOrEmpty(x.Value))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion

        #region Конструкторы

        public BotConfigJsonProvider() { }

        public BotConfigJsonProvider(string configPath)
        {
            SetConfigPath(configPath);
        }

        #endregion
    }
}
