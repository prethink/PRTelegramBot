using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class ReplyDynamicCommandHandler : ReplyCommandHandler, IMessageCommandHandler
    {
        #region Базовый класс

        /// <inheritdoc />
        protected override Dictionary<string, CommandHandler> GetCommands(IBotContext context)
        {
            var currentHandler = context.Current.Handler as Handler;
            if (currentHandler == null)
                return new();

            return currentHandler.ReplyDynamicCommandsStore.Commands;
        }

        #endregion
    }
}
