using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace AspNetExample.BotController
{
    public class BotHandlerWithoutDependency
    {
        public BotHandlerWithoutDependency()
        {

        }

        [ReplyMenuHandler("Testnodi")]
        public async Task TestMethodWithoutDependency(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, $"{nameof(TestMethodWithoutDependency)}");
        }

        [SlashHandler("/Testnodi")]
        public async Task SlashNoDi(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(SlashNoDi));
        }
    }
}
