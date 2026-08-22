using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.TestModels.TestHandlers
{
    internal class MessageTestHandler : IMessageCommandHandler
    {
        public async Task<UpdateResult> Handle(IBotContext context, Message updateType)
        {
            ///* If the data you need has arrived and you have handled it, return the Handled result. 
            // * This means the action has been handled and the remaining handlers are skipped. */
            //if (updateType.Text == "The data you need")
            //    return UpdateResult.Handled;

            //// The command was not handled; let the next handler try.
            return UpdateResult.Continue;
        }
    }
}
