using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class InlineCommandHandler : ICallbackQueryCommandHandler
    {
        /// <inheritdoc />
        public async Task<UpdateResult> Handle(IBotContext context, CallbackQuery updateType)
        {
            var command = InlineCallback.GetCommandByCallbackOrNull(updateType.Data);
            if (command is not null)
            {
                context.Current.Events.CommandsEvents.OnPreInlineCommandHandleInvoke(context.CreateBotEventArgs());
                var executer = new ExecutorCallbackQueryCommand(context.Current);
                var currentHandler = context.Current.Handler as Handler;
                if (currentHandler is null)
                    return UpdateResult.Continue;

                var resultExecute = await executer.Execute(command.CommandType, context, currentHandler.CallbackQueryCommandsStore.Commands);
                if (resultExecute == CommandResult.Executed)
                {
                    context.Current.Events.CommandsEvents.OnPostInlineCommandHandleInvoke(context.CreateBotEventArgs());
                    return UpdateResult.Handled;
                }
            }
            return UpdateResult.Continue;
        }
    }
}
