using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class InlineClassInstanceHandler : ICallbackQueryCommandHandler
    {
        #region ICallbackQueryCommandHandler

        public async Task<UpdateResult> Handle(PRBotBase bot, Update update, CallbackQuery updateType)
        {
            foreach (var handler in bot.InlineClassHandlerInstances)
            {
                var command = InlineCallback.GetCommandByCallbackOrNull(updateType.Data);
                if (command != null && Convert.ToInt32(command.CommandType) == Convert.ToInt32(handler.Key))
                    return await handler.Value.Handle(bot, update, updateType);
            }

            return UpdateResult.Continue;
        }

        #endregion
    }
}
