using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Кастомные настройки
    /// </summary>
    public class TextConfig
    {
        public Dictionary<string, string> Variables { get; set; } = new();
        public Dictionary<string, string> Messages { get; set; } = new();
        public Dictionary<string, string> Buttons { get; set; } = new();

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
                //var showErrors = ConfigApp.GetSettingsTelegram<TelegramConfig>().ShowErrorNotFoundNameButton;
                //if (showErrors)
                //{
                //    Console.WriteLine($"Обнаружена проблема в кнопке {result} | вызов {methodName}");
                //}
                Console.WriteLine($"Обнаружена проблема в кнопке {result} | вызов {methodName}");

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
