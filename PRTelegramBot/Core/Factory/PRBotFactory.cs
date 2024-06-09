using PRTelegramBot.Configs;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.Factory
{
    public class PRBotFactory : PRBotFactoryBase
    {
        public override IPRBot CreateBot(TelegramOptions options)
        {
            return new PRBot(options);
        }
    }
}
