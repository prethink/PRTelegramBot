using PRTelegramBot.Configs;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.Factory
{
    public abstract class PRBotFactoryBase
    {
        public abstract IPRBot CreateBot(TelegramOptions options);
    }
}
