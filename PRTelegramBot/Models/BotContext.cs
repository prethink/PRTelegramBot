using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models
{
    public class BotContext
    {
        public ITelegramBotClient BotClient { get; protected set; }
        public Update Update { get; protected set; }
        public CancellationToken CancellationToken { get; protected set; }

        public BotContext(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            BotClient = botClient;
            Update = update;
            CancellationToken = cancellationToken;
        }
    }
}
