using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factory
{
    public class PRBotWebHookFactory : PRBotFactoryBase
    {
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PRBotWebHook(options);
        }
    }
}
