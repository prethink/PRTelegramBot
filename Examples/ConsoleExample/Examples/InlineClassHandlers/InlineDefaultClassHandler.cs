using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.TCommands;
using Telegram.Bot.Types;

namespace ConsoleExample.Examples.InlineClassHandlers
{
    /// <summary>
    /// Пример класса обработки inline команды.
    /// </summary>
    public class InlineDefaultClassHandler : ICallbackQueryCommandHandler
    {
        #region Константы

        public const string TEST_ADD_MESSAGE = "Данные из класса, здесь может быть какая-нибудь обработка...";

        #endregion

        #region ICallbackQueryCommandHandler

        public async Task<UpdateResult> Handle(IBotContext context, CallbackQuery updateType)
        {
            var command = context.GetCommandByCallbackOrNull<StringTCommand>();
            if (command != null)
            {
                await PRTelegramBot.Helpers.Message.Send(context, $"{TEST_ADD_MESSAGE} {command.Data.StrData}");
                return UpdateResult.Handled;
            }

            return UpdateResult.Continue;
        }

        #endregion
    }
}
