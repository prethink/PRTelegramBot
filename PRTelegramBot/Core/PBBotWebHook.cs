using PRTelegramBot.Configs;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using Telegram.Bot;

namespace PRTelegramBot.Core
{
    public class PBBotWebHook : IPRBot
    {
        public TelegramOptions Options => throw new NotImplementedException();

        public TEvents Events => throw new NotImplementedException();

        public RegisterCommands Register => throw new NotImplementedException();

        public Handler Handler => throw new NotImplementedException();

        public ITelegramBotClient botClient => throw new NotImplementedException();

        public string BotName => throw new NotImplementedException();

        public long? TelegramId => throw new NotImplementedException();

        public long BotId => throw new NotImplementedException();

        public Task Start()
        {
            throw new NotImplementedException();
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }
    }
}
