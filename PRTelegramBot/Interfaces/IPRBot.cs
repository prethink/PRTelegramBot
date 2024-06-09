using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Models;
using Telegram.Bot;

namespace PRTelegramBot.Interfaces
{
    public interface IPRBot
    {
        public string BotName { get; }
        public long? TelegramId { get; }
        public long BotId { get; }
        public TelegramOptions Options { get; }
        public TEvents Events { get; }
        public RegisterCommands Register { get; }
        public Handler Handler { get; }
        public ITelegramBotClient botClient { get; }
        public Task Start();

        public Task Stop();
    }
}
