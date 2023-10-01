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

        public ConfigApp(string pathConfig, string baseDir = "")
        {
            if(string.IsNullOrWhiteSpace(baseDir))
            {
                string assemblyPath = Assembly.GetEntryAssembly().Location;
                baseDir = Path.GetDirectoryName(assemblyPath);
            }

            config = new ConfigurationBuilder()
                     .SetBasePath(baseDir)
                     .AddJsonFile(pathConfig).Build();
        }

        public T GetSettings<T>()
        {
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }

        public static T GetSettings<T>(IConfigurationRoot config)
        {
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }


    }



}