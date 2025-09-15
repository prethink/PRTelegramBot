using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;

namespace AspNetExample.BotController
{
    [BotHandler]
    public class BotHandlerOnlyStatic
    {
        [ReplyMenuHandler("TestStatic")]
        public async static Task StaticTestMethod(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, nameof(StaticTestMethod));
        }
    }
}
