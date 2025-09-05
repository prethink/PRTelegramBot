using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
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

        public async Task<UpdateResult> Handle(PRBotBase bot, Update update, CallbackQuery updateType)
        {
            var command = InlineCallback<StringTCommand>.GetCommandByCallbackOrNull(updateType.Data);
            if (command != null)
            {
                await PRTelegramBot.Helpers.Message.Send(bot.BotClient, update.GetChatId(), $"{TEST_ADD_MESSAGE} {command.Data.StrData}");
                return UpdateResult.Handled;
            }

            return UpdateResult.Continue;
        }

        #endregion
    }
}
