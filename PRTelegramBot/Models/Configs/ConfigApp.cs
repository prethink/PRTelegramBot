using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PRTelegramBot.Models.Configs
{
    /// <summary>
    /// Работа с JSON файла
    /// </summary>
    public class ConfigApp : BaseConfig
    {

        public ConfigApp(string pathConfig)
        {
            if (!File.Exists(pathConfig))
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
                };

                // Сериализуем объект TextConfig в JSON
                string json = JsonConvert.SerializeObject(new { TextConfig = config }, Formatting.Indented);
                string directoryPath = Path.GetDirectoryName(pathConfig);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                File.WriteAllText(pathConfig, json);
            }

            config = new ConfigurationBuilder()
                     .AddJsonFile(pathConfig).Build();
        }

    }



}