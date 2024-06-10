using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    public class BotEventArgs : EventArgs
    {
        public PRBotBase Bot { get; private set; }
        public ITelegramBotClient BotClient { get; private set; }
        public Update Update { get; private set; }

        public BotEventArgs(PRBotBase bot, Update update)
        {
            Bot = bot; 
            BotClient = bot.botClient;
            Update = update;
        }
    }
}
