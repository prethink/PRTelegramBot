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
        public List<string> Commands { get;  set; }

        /// <summary>
        /// Приоритетная команда
        /// </summary>
        public bool Priority { get;  set; }


        public ReplyMenuHandlerAttribute(bool priority, params string[] commands) : base(0)
        {
            Init(priority, commands);
        }

        public ReplyMenuHandlerAttribute(params string[] commands) : base(0)
        {
            Init(true, commands);
        }

        public ReplyMenuHandlerAttribute(bool priority,long botId, params string[] commands) : base(botId)
        {
            Init(priority, commands);
        }

        public ReplyMenuHandlerAttribute(long botId, params string[] commands) : base(botId)
        {
            Init(true, commands);
        }

        public virtual void Init(bool priority, params string[] commands)
        {
            Commands = commands.ToList();
            for (int i = 0; i < Commands.Count; i++)
            {
                    Commands[i] = commands[i]; 
            }
            Priority = priority;
        }


    }
}
