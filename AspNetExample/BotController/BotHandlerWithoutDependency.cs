using PRTelegramBot.Attributes;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AspNetExample.BotController
{
    [BotHandler]
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

        [SlashHandler(CommandComparison.Equals, "/Testnodi")]
        public async Task SlashNoDi(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(SlashNoDi));
        }
    }
}
