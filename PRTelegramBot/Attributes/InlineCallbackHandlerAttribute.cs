using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Аттрибут для inline команд
    /// </summary>
    public class InlineCallbackHandlerAttribute<T> : BaseQueryAttribute where T : Enum
    {
        /// <summary>
        /// Коллекция inline команд
        /// </summary>
        public List<Enum> Commands { get; private set; }

        public InlineCallbackHandlerAttribute(params T[] commands) : base(0)
        {
            Commands = new List<Enum>();
            foreach (var command in commands) 
            { 
                Commands.Add((Enum)command);
            }   
        }

        public InlineCallbackHandlerAttribute(long botId, params T[] commands) : base(botId)
        {
            Commands = new List<Enum>();
            foreach (var command in commands)
            {
                Commands.Add((Enum)command);
            }
        }
    }
}
