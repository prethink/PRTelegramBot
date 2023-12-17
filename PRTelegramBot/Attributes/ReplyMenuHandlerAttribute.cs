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

        public ReplyMenuHandlerAttribute(params string[] commands) : base(0)
        {
            Init(commands);
        }

        public ReplyMenuHandlerAttribute(long botId, params string[] commands) : base(botId)
        {
            Init(commands);
        }

        public virtual void Init(params string[] commands)
        {
            Commands = commands.ToList();
            for (int i = 0; i < Commands.Count; i++)
            {
                Commands[i] = commands[i]; 
            }
        }
    }
}
