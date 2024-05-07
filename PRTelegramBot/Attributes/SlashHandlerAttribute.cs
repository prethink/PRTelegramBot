using System.Collections.Generic;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для работы слэш (/) команд
    /// </summary>
    public class SlashHandlerAttribute : BaseQueryAttribute<string>
    {

        public SlashHandlerAttribute(params string[] commands) 
            : this(0, commands) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="botId"></param>
        /// <param name="commands"></param>
        public SlashHandlerAttribute(long botId, params string[] commands) : base(botId)
        {
            foreach (var command in commands)
            {
                var formatedCommand = command.StartsWith("/") ? command : "/" + command;
                Commands.Add(formatedCommand);
            }
        }
    }
}
