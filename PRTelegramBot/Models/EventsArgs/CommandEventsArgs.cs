using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    public class CommandEventsArgs : BotEventArgs
    {
        public Func<ITelegramBotClient, Update, Task> ExecuteMethod { get; private set; }
        public CommandEventsArgs(PRBot bot, Update update, Func<ITelegramBotClient, Update, Task> executeMethod) 
            : base(bot, update)
        {
            this.ExecuteMethod = executeMethod;
        }
    }
}
