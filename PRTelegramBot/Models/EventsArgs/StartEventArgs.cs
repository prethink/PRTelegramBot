using PRTelegramBot.Core;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    public class StartEventArgs : BotEventArgs
    {
        public string Data;

        public StartEventArgs(PRBot bot, Update update, string data)
            : base(bot, update)
        {
            Data = data;
        }
    }
}
