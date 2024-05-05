using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;

namespace PRTelegramBot.Configs
{
    public class BotConfigJsonProvider : IBotConfigProvider
    {
        private IConfigurationRoot config { get; set; }

        public BotConfigJsonProvider() { }

        public BotConfigJsonProvider(string configPath)
        {
            SetConfigPath(configPath);
        }

        public TReturn GetValue<TReturn>(string section)
        {
            return config.GetSection(section).Get<TReturn>();
        }

        public T GetSettings<T>()
        {
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }

        public T GetSettings<T>(IConfigurationRoot config)
        {
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }

        public void SetConfigPath(string configPath)
        {
            config = new ConfigurationBuilder()
                .AddJsonFile(configPath).Build();
        }

        public string GetValueByKey<T>(string key) where T : class
        {
            var sectionData = GetSettings<T>();
            var propertyInfo = typeof(T).GetProperty(key);
            var value = propertyInfo.GetValue(sectionData);
            return (string)value;
        }
    }
}
