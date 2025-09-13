using PRTelegramBot.Core.Executors;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.CommandHandlers
{
    internal class SlashCommandHandler : IMessageCommandHandler
    {
        #region IMessageCommandHandler

        /// <inheritdoc />
        public async Task<UpdateResult> Handle(IBotContext context, Message updateType)
        {
            string command = context.Update.Message.Text;
            if (command.StartsWith('/'))
            {
                var resultExecute = StartHasDeepLink(context, command);
                if (resultExecute == CommandResult.Executed)
                    return UpdateResult.Handled;

                context.Current.Events.CommandsEvents.OnPreSlashCommandHandleInvoke(context.CreateBotEventArgs());

                var executer = new ExecutorSlashCommand(context.Current);
                var currentHandler = context.Current.Handler as Handler;
                if (currentHandler == null)
                    return UpdateResult.Continue;

                resultExecute = await executer.Execute(command, context, currentHandler.SlashCommandsStore.Commands);

                if (resultExecute != CommandResult.Continue)
                {
                    context.Current.Events.CommandsEvents.OnPostSlashCommandHandleInvoke(context.CreateBotEventArgs());
                    return UpdateResult.Handled;
                }
            }
            return UpdateResult.Continue;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверка является ли команда start с аргументом.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="command">Команда.</param>
        /// <returns>Результат выполнение команд.</returns>
        private CommandResult StartHasDeepLink(IBotContext context, string command)
        {
            try
            {
                if (!command.ToLower().Contains("start") && command.Contains(" "))
                    return CommandResult.Continue;

                var spl = command.Split(' ');
                if (spl.Length < 2 || string.IsNullOrEmpty(spl[1]))
                    return CommandResult.Continue;

                context.Current.Events.OnUserStartWithArgsInvoke(new StartEventArgs(context, spl[1]));
                return CommandResult.Executed;
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
                return CommandResult.Error;
            }
        }

        #endregion
    }
}
