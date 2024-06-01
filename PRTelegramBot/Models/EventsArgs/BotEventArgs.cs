using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    public class BotEventArgs : EventArgs
    {
        public PRBot Bot { get; private set; }
        public ITelegramBotClient BotClient { get; private set; }
        public Update Update { get; private set; }

        public BotEventArgs(PRBot bot, Update update)
        {
            Bot = bot; 
            BotClient = bot.botClient;
            Update = update;
        }
    }
}
