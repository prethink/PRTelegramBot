using PRTelegramBot.Configs;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.Factory
{
    public class PRBotWebHookFactory : PRBotFactoryBase
    {
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PBBotWebHook(options);
        }
    }
}
