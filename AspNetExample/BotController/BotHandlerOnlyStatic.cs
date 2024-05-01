using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AspNetExample.BotController
{
    [BotHandler]
    public class BotHandlerOnlyStatic
    {
        [ReplyMenuHandler("TestStatic")]
        public async static Task StaticTestMethod(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(StaticTestMethod));
        }
    }
}
