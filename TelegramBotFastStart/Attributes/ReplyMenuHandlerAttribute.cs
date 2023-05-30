using PRTelegramBot.Configs;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для reply методов
    /// </summary>
    public class ReplyMenuHandlerAttribute : Attribute
    {
        /// <summary>
        /// Список reply команд
        /// </summary>
        public List<string> Commands { get; set; }

        /// <summary>
        /// Приоритетная команда
        /// </summary>
        public bool Priority { get; private set; }

        /// <summary>
        /// Создание новой команды
        /// </summary>
        /// <param name="priority">Будут проигнорированы следующие шаги да нет</param>
        /// <param name="commands">Набор команд</param>
        public ReplyMenuHandlerAttribute(bool priority, params string[] commands)
        {
            Commands = commands.Select(x => GetNameFromResourse(x)).ToList();
            for (int i = 0; i < Commands.Count; i++)
            {
                if (Commands[i].Contains("NOT_FOUND"))
                {
                    Commands[i] = commands[i];
                }
            }
            Priority = priority;
        }

        private static string GetNameFromResourse(string command)
        {
            return ConfigApp.GetSettings<CustomSettings>().GetButton(command);
        }
    }
}
