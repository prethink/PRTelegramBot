using PRTelegramBot.Core.Executors;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class ReplyCommandHandler : IMessageCommandHandler
    {
        public async Task<UpdateResult> Handle(PRBotBase bot, Update update, Message updateType)
        {
            string command = updateType.Text;
            RemoveBracketsIfExists(ref command);
            bot.Events.CommandsEvents.OnPreReplyCommandHandleInvoke(new BotEventArgs(bot, update));

            var executer = new ExecutorReplyCommand(bot);
            var currentHandler = bot.Handler as Handler;
            if (currentHandler == null)
                return UpdateResult.Continue;

            var resultExecute = await executer.Execute(command, update, GetCommands(bot));
            if (resultExecute != CommandResult.Continue)
            {
                bot.Events.CommandsEvents.OnPostReplyCommandHandleInvoke(new BotEventArgs(bot, update));
                return UpdateResult.Handled;
            }
            return UpdateResult.Continue;
        }

        protected virtual Dictionary<string, CommandHandler> GetCommands(PRBotBase bot)
        {
            var currentHandler = bot.Handler as Handler;
            if (currentHandler == null)
                return new();

            return currentHandler.ReplyCommandsStore.Commands;
        } 

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
