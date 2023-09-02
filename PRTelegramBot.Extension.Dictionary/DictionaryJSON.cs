using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Extension.Dictionary
{
    /// <summary>
    /// Позволяет редактировать текст сообщений не перекомпилируя программу,а используя json файл
    /// </summary>
    public class DictionaryJSON
    {
        /// <summary>
        /// Преобразует константу сообщения в текст из JSOON
        /// </summary>
        /// <param name="messagePattern"></param>
        /// <returns></returns>
        public static string GetMessage(string messagePattern)
        {
            return ConfigApp.GetSettings<TextConfig>().GetMessage(messagePattern);
        }

        /// <summary>
        /// Преобразует константу команды в текст из JSON
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string GetButton(string command)
        {
            var result = ConfigApp.GetSettings<TextConfig>().GetButton(command);
            return result.Contains("NOT_FOUND") ? command : result;
        }
    }
}
