using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factory
{
    public abstract class PRBotFactoryBase
    {
        public abstract PRBotBase CreateBot(TelegramOptions options);
    }
}
