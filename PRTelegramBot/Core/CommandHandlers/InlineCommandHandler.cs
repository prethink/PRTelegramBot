using PRTelegramBot.Core.Executors;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class InlineCommandHandler : ICallbackQueryCommandHandler
    {
        public async Task<UpdateResult> Handle(PRBotBase bot, Update update, CallbackQuery updateType)
        {
            var command = InlineCallback.GetCommandByCallbackOrNull(updateType.Data);
            if (command != null)
            {
                bot.Events.CommandsEvents.OnPreInlineCommandHandleInvoke(new BotEventArgs(bot, update));
                var executer = new ExecutorCallbackQueryCommand(bot);
                var currentHandler = bot.Handler as Handler;
                if (currentHandler == null)
                    return UpdateResult.Continue;

                var resultExecute = await executer.Execute(command.CommandType, update, currentHandler.CallbackQueryCommandsStore.Commands);
                if (resultExecute == CommandResult.Executed)
                {
                    bot.Events.CommandsEvents.OnPostInlineCommandHandleInvoke(new BotEventArgs(bot, update));
                    return UpdateResult.Handled;
                }
            }
            return UpdateResult.Continue;
        }
    }
}
