using PRTelegramBot.Configs;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для reply методов
    /// </summary>
    public class ReplyMenuHandlerAttribute : BaseQueryAttribute
    {
        /// <summary>
        /// Список reply команд
        /// </summary>
        public List<string> Commands { get; private set; }

        /// <summary>
        /// Приоритетная команда
        /// </summary>
        public bool Priority { get; private set; }


        public ReplyMenuHandlerAttribute(bool priority, params string[] commands) : base(0)
        {
            Init(priority, commands);
        }

        public ReplyMenuHandlerAttribute(bool priority,long botId, params string[] commands) : base(botId)
        {
            Init(priority, commands);
        }

        private void Init(bool priority, params string[] commands)
        {
            Commands = commands.ToList();
            for (int i = 0; i < Commands.Count; i++)
            {
                    Commands[i] = commands[i]; 
            }
            Priority = priority;

            //Commands = commands.Select(x => GetNameFromResourse(x)).ToList();
            //for (int i = 0; i < Commands.Count; i++)
            //{
            //    if (Commands[i].Contains("NOT_FOUND"))
            //    {
            //        Commands[i] = commands[i];
            //    }
            //}
            //Priority = priority;
        }

        //private static string GetNameFromResourse(string command)
        //{
        //    return ConfigApp.GetSettings<TextConfig>().GetButton(command);
        //}
    }
}
