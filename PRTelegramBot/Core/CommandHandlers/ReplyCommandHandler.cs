using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class ReplyCommandHandler : IMessageCommandHandler
    {
        #region IMessageCommandHandler

        /// <inheritdoc />
        public async Task<UpdateResult> Handle(IBotContext context, Message updateType)
        {
            string command = updateType.Text;
            RemoveBracketsIfExists(ref command);
            context.Current.Events.CommandsEvents.OnPreReplyCommandHandleInvoke(context.CreateBotEventArgs());

            var executer = new ExecutorReplyCommand(context.Current);
            var currentHandler = context.Current.Handler as Handler;
            if (currentHandler is null)
                return UpdateResult.Continue;

            var resultExecute = await executer.Execute(command, context, GetCommands(context));
            if (resultExecute != CommandResult.Continue)
            {
                context.Current.Events.CommandsEvents.OnPostReplyCommandHandleInvoke(context.CreateBotEventArgs());
                return UpdateResult.Handled;
            }
            return UpdateResult.Continue;
        }

        protected virtual Dictionary<string, CommandHandler> GetCommands(IBotContext context)
        {
            var currentHandler = context.Current.Handler as Handler;
            if (currentHandler is null)
                return new();

            return currentHandler.ReplyCommandsStore.Commands;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удалить скобки из сообщения
        /// </summary>
        /// <param name="command"></param>
        protected void RemoveBracketsIfExists(ref string command)
        {
            if (command.Contains("(") && command.Contains(")"))
                command = command.Remove(command.LastIndexOf("(") - 1);
        }

        #endregion
    }
}
