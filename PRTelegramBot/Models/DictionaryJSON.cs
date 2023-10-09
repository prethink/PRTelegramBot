using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRTelegramBot.Models.Configs;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Позволяет редактировать текст сообщений не перекомпилируя программу,а используя json файл
    /// </summary>
    public class DictionaryJSON
    {
        private ConfigApp _config { get; set; }
        public DictionaryJSON(ConfigApp config)
        {
            _config = config;
        }

        public DictionaryJSON(string path)
        {
            _config = new ConfigApp(path);
        }

        /// <summary>
        /// Преобразует константу сообщения в текст из JSOON
        /// </summary>
        /// <param name="messagePattern"></param>
        /// <returns></returns>
        public string GetMessage(string messagePattern)
        {
            return _config.GetSettings<TextConfig>().GetMessage(messagePattern);
        }

        /// <summary>
        /// Преобразует константу команды в текст из JSON
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string GetButton(string command)
        {
            var result = _config.GetSettings<TextConfig>().GetButton(command);
            return result.Contains("NOT_FOUND") ? command : result;
        }
    }
}
