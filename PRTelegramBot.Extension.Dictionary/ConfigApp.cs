using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PRTelegramBot.Extension.Dictionary
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



}