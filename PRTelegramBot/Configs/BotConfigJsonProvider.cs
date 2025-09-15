﻿using Microsoft.Extensions.Configuration;
using PRTelegramBot.Interfaces;
using System.Text.Json;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Провайдер работы с конфигурационными json файлами.
    /// </summary>
    public sealed class BotConfigJsonProvider : IBotConfigProvider
    {
        #region Поля и свойства

        private IConfigurationRoot configuration { get; set; }

        /// <summary>
        /// Путь до json файла.
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
