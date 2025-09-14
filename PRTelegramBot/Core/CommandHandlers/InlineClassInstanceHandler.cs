using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class InlineClassInstanceHandler : ICallbackQueryCommandHandler
    {
        #region ICallbackQueryCommandHandler

        /// <inheritdoc/>
        public async Task<UpdateResult> Handle(IBotContext context, CallbackQuery updateType)
        {
            foreach (var handler in context.Current.InlineClassHandlerInstances)
            {
                var command = InlineCallback.GetCommandByCallbackOrNull(updateType.Data);
                if (command is not null && Convert.ToInt32(command.CommandType) == Convert.ToInt32(handler.Key))
                    return await handler.Value.Handle(context, updateType);
            }

            return UpdateResult.Continue;
        }

        #endregion
    }
}
