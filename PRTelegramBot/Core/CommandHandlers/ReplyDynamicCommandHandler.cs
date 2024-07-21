using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class ReplyDynamicCommandHandler : ReplyCommandHandler, IMessageCommandHandler
    {
        protected override Dictionary<string, CommandHandler> GetCommands(PRBotBase bot)
        {
            var currentHandler = bot.Handler as Handler;
            if (currentHandler == null)
                return new();

            return currentHandler.ReplyDynamicCommandsStore.Commands;
        }
    }
}
