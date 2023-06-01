using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Работа с JSON файла
    /// </summary>
    public class ConfigApp
    {
        public IConfigurationRoot config { get; }
        public IConfigurationRoot telegramConfig { get; }

        private static ConfigApp Instance;

        public static ConfigApp GetInstance()
        {
            if (Instance == null)
                Instance = new ConfigApp();
            return Instance;
        }

        private ConfigApp()
        {
            string assemblyPath = Assembly.GetEntryAssembly().Location;
            string basedir = Path.GetDirectoryName(assemblyPath);
            string configJson = "Configs\\appconfig.json";
            string telegramJson = "Configs\\telegram.json";

            string fullPathAppConfig = Path.Combine(basedir, configJson);
            string fullPathTelegramConfig = Path.Combine(basedir, telegramJson);

            if (!File.Exists(fullPathAppConfig))
            {
                CopyTemplate("PRTelegramBot.Configs.appconfig.json", fullPathAppConfig);
            }

            if (!File.Exists(fullPathTelegramConfig))
            {
                CopyTemplate("PRTelegramBot.Configs.telegram.json", fullPathTelegramConfig);
            }

            config = new ConfigurationBuilder()
                     .SetBasePath(basedir)
                     .AddJsonFile(configJson).Build();

            telegramConfig = new ConfigurationBuilder()
                     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                     .AddJsonFile(telegramJson).Build();
        }

        public void CopyTemplate(string resourceName, string pathFile)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception("Ресурсный файл не найден.");
                }
                Directory.CreateDirectory(Path.GetDirectoryName(pathFile));
                using (FileStream fileStream = new FileStream(pathFile, FileMode.Create))
                {
                    stream.CopyTo(fileStream);
                }
            }
        }

        public static T GetSettings<T>()
        {
            var config = GetInstance().config;
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }

        public static T GetSettingsTelegram<T>()
        {
            var config = GetInstance().telegramConfig;
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }
    }


    /// <summary>
    /// Настройки телеграма
    /// </summary>
    public class TelegramConfig
    {
        public string Token { get; set; }
        public List<long> WhiteListUsers { get; set; } 
        public List<long> Admins { get; set; }
        public bool ShowErrorNotFoundNameButton { get; set; }
    }

    /// <summary>
    /// Кастомные настройки
    /// </summary>
    public class CustomSettings
    {
        public Dictionary<string, string> Variables { get; set; }
        public Dictionary<string, string> Messages { get; set; }
        public Dictionary<string, string> Buttons { get; set; }

        public string GetVariable(string command, [CallerMemberName] string methodName = "")
        {
            //FixVariables(Variables);
            string value;
            Variables.TryGetValue(command, out value);
            var result = value ?? $"NOT_FOUND_{command}";
            if (result.Contains("NOT_FOUND"))
            {
                Console.WriteLine($"Обнаружена проблема в переменной {result} | вызов {methodName}");
            }
            return result;
        }

        public string GetMessage(string message, [CallerMemberName] string methodName = "")
        {
            string value;
            Messages.TryGetValue(message, out value);
            var result = value != null ? GetFormatedMessage(value) : $"NOT_FOUND_{message}";
            if (result.Contains("NOT_FOUND"))
            {
                Console.WriteLine($"Обнаружена проблема в сообщение {result} | вызов {methodName}");
            }
            return result;
        }

        public string GetButton(string button, [CallerMemberName] string methodName = "")
        {
            string value;
            Buttons.TryGetValue(button, out value);
            var result = value ?? $"NOT_FOUND_{button}";
            if (result.Contains("NOT_FOUND"))
            {
                //TODO: Поправить
                var showErrors = ConfigApp.GetSettingsTelegram<TelegramConfig>().ShowErrorNotFoundNameButton;
                if (showErrors)
                {
                    Console.WriteLine($"Обнаружена проблема в кнопке {result} | вызов {methodName}");
                }

            }
            return result;
        }

        public string GetFormatedMessage(string text)
        {
            var pattern = @"{[A-za-z0-9]*}";
            var match = Regex.Matches(text, pattern);

            if (match.Count > 0)
            {
                foreach (var varible in match)
                {
                    var result = varible.ToString();
                    text = text.Replace(result, GetVariable(result.Replace("{", "").Replace("}", "")));
                }
            }

            return text;
        }
    }
}
