using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.TestModels.TestHandlers
{
    internal class MessageTestHandler : IMessageCommandHandler
    {
        public async Task<UpdateResult> Handle(PRBotBase bot, Update update, Message updateType)
        {
            ///* Если данные пришли которые вам нужны и вы их обработали возращаем результат Handled. 
            // * Это будет означать, то что действие выполнено и остальные обработчики будут пропущены. */
            //if (updateType.Text == "Нужные данные")
            //    return UpdateResult.Handled;

            //// Команда не обработана, попытаемся обработать следующим обработчиком.
            return UpdateResult.Continue;
        }
    }
}
