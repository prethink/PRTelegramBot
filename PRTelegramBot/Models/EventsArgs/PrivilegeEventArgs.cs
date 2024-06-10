using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    public class PrivilegeEventArgs : CommandEventsArgs
    {
        public int? Mask { get; private set; }
        public PrivilegeEventArgs(PRBotBase bot, Update update, Func<ITelegramBotClient, Update, Task> executeMethod, int? mask)
            : base(bot, update, executeMethod)
        {
            Mask = mask;
        }
    }
}
