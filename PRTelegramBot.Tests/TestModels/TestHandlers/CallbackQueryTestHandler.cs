using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.TestModels.TestHandlers
{
    public class CallbackQueryTestHandler : ICallbackQueryCommandHandler
    {
        public async Task<UpdateResult> Handle(PRBotBase bot, Update update, CallbackQuery updateType)
        {
            ///* Если данные пришли которые вам нужны и вы их обработали возращаем результат Handled. 
            // * Это будет означать, то что действие выполнено и остальные обработчики будут пропущены. */
            //if (updateType.Data == "Нужные данные")
            //    return UpdateResult.Handled;

            //// Команда не обработана, попытаемся обработать следующим обработчиком.
            return UpdateResult.Continue;
        }
    }
}
