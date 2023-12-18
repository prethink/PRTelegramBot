using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace AspNetExample.BotController
{
    public class BotHandlerOnlyStatic
    {
        [ReplyMenuHandler("TestStatic")]
        public async static Task StaticTestMethod(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(StaticTestMethod));
        }
    }
}
