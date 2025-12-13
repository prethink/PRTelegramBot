using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services.Messages;

namespace AspNetExample.BotController
{
    [BotHandler]
    public class BotHandlerWithoutDependency
    {
        public BotHandlerWithoutDependency()
        {

        }

        [ReplyMenuHandler("Testnodi")]
        public async Task TestMethodWithoutDependency(IBotContext context)
        {
            await MessageSender.Send(context, $"{nameof(TestMethodWithoutDependency)}");
        }

        [SlashHandler(CommandComparison.Equals, "/Testnodi")]
        public async Task SlashNoDi(IBotContext context)
        {
            await MessageSender.Send(context, nameof(SlashNoDi));
        }
    }
}
