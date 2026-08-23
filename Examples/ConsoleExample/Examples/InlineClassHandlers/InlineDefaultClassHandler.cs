using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Services.Messages;
using Telegram.Bot.Types;

namespace ConsoleExample.Examples.InlineClassHandlers
{
    /// <summary>
    /// Example of a class that handles an inline command.
    /// </summary>
    public class InlineDefaultClassHandler : ICallbackQueryCommandHandler
    {
        #region Constants

        public const string TEST_ADD_MESSAGE = "Data from the class; some processing could happen here...";

        #endregion

        #region ICallbackQueryCommandHandler

        public async Task<UpdateResult> Handle(IBotContext context, CallbackQuery updateType)
        {
            var command = context.GetCommandByCallbackOrNull<StringTCommand>();
            if (command != null)
            {
                await MessageSender.Send(context, $"{TEST_ADD_MESSAGE} {command.Data.StrData}");
                return UpdateResult.Handled;
            }

            return UpdateResult.Continue;
        }

        #endregion
    }
}
