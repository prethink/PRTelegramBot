using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using System.Linq.Expressions;

namespace PRTelegramBot.Core
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
            if (!File.Exists(configPath))
            {
                // Создаем экземпляр класса TextConfig и заполняем его значениями
                TextConfig config = new TextConfig
                {
                    Messages = new Dictionary<string, string>
                    {
                        {"MSG_MAIN_MENU", "Главное меню"},
                    },
                    Buttons = new Dictionary<string, string>
                    {
                        {"RP_MAIN_MENU", "🏠 Главное меню"},
                    }
                    ,
                    Variables = new Dictionary<string, string>
                    {
                        {"Promo", "Test"},
                    }
                };

                // Сериализуем объект TextConfig в JSON
                string json = JsonConvert.SerializeObject(new { TextConfig = config }, Formatting.Indented);
                string directoryPath = Path.GetDirectoryName(configPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                File.WriteAllText(configPath, json);
            }

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
