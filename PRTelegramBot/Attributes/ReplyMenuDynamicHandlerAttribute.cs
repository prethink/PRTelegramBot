using PRTelegramBot.Core;

namespace PRTelegramBot.Attributes
{
    public class ReplyMenuDynamicHandlerAttribute : ReplyMenuHandlerAttribute 
    {
        public ReplyMenuDynamicHandlerAttribute(params string[] commands)
            : this(0, commands) { }

        public ReplyMenuDynamicHandlerAttribute(long botId, params string[] commands) : base(botId, commands)
        {
            var bot = BotCollection.Instance.GetBotOrNull(botId);
            var dynamicCommand = bot.Options.ReplyDynamicCommands;
            foreach (var command in commands) 
            {
                if(!dynamicCommand.ContainsKey(command))
                {
                    //bot.InvokeErrorLog();
                    continue;
                }
                Commands.Add(dynamicCommand[command]);
            }
        }
    }
}
