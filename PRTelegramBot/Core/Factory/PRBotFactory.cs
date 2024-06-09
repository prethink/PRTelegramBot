using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factory
{
    public class PRBotFactory : PRBotFactoryBase
    {
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PRBot(options);
        }
    }
}
