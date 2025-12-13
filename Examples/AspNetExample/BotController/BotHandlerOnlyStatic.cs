using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

namespace AspNetExample.BotController
{
    [BotHandler]
    public class BotHandlerOnlyStatic
    {
        [ReplyMenuHandler("TestStatic")]
        public async static Task StaticTestMethod(IBotContext context)
        {
            await MessageSender.Send(context, nameof(StaticTestMethod));
        }
    }
}
